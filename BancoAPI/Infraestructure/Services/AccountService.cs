using Application.Dtos.Users;
using Application.enums;
using Application.Exceptions;
using Application.Interface;
using Application.Wrappers;
using Domain.Settings;
using Infraestructure.CustomEntities;
using Infraestructure.Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, 
            SignInManager<ApplicationUser> signInManager, JWTSettings jwtSettings, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string apiAddress)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) 
            {
                throw new ApiExceptions($"El usuario con el email {request.Email} no existe");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded) 
            {
                throw new ApiExceptions($"El email o la contraseña no son válidos");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJwtTokenAsync(user);
            AuthenticationResponse response = new();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = roleList.ToList();
            response.IsVerified = user.EmailConfirmed;

            var refreshToken = GenerateRefreshToken(apiAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Autenticado {user.UserName}");
        }
        private async Task<JwtSecurityToken> GenerateJwtTokenAsync (ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();

            for (int i = 0; i<roles.Count; i++)
            {
                rolesClaims.Add(new("roles", roles[i]));
            }
            string ipAddress = IpHelper.GetApiAddress();

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("uid", user.Id),
                new("ip", ipAddress),
            }
            .Union(userClaims).Union(rolesClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signInCredentials = new SigningCredentials( symetricSecurityKey, SecurityAlgorithms.HmacSha256 );
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signInCredentials
                );

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken (string apiAddress)
        {
            return new()
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = apiAddress,
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServicesProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServicesProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithUserName != null) 
            {
                throw new ApiExceptions($"El usuario {request.UserName} ya existe");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmail != null)
            {
                throw new ApiExceptions($"El email {request.UserName} ya ha sido registrado");
            }
            else
            {
                var result = await _userManager.CreateAsync(user,request.Password);   
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    return new Response<string>(user.Id, message: $"Usuario registrado exitosamente.");
                }
                else
                {
                    throw new ApiExceptions($"{result.Errors}.");
                }
            }
        }
    }
}
