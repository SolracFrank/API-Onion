using Application.Dtos.Users;
using Application.Wrappers;

namespace Application.Interface
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string apiAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
    }
}
