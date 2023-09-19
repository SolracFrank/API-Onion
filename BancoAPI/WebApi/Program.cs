using Application;
using Infraestructure.CustomEntities;
using Infraestructure.Identity.Seeds;
using Infraestructure.Persistance;
using Microsoft.AspNetCore.Identity;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Infraestructure
builder.Services.AddPersistanceInfraestructure(builder.Configuration);
builder.Services.AddIdentityExtension(builder.Configuration);
//Application
builder.Services.AddApplication();
//Extensions
builder.Services.AddApiVersioningExtension();
//cors builder
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build => {
    build.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure Identity
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception)
    {
        throw;
    }
};

app.UseHttpsRedirection();
//  CORS 
app.UseCors("PolicyCors"); 
app.UseRouting();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();
app.MapControllers();

app.Run();
