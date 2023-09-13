using Application;
using Infraestructure.Persistance;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Infraestructure
builder.Services.AddPersistanceInfraestructure(builder.Configuration);
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

app.UseHttpsRedirection();
//  CORS 
app.UseCors("PolicyCors"); 
app.UseRouting();
app.UseAuthorization();
app.UseErrorHandlingMiddleware();
app.MapControllers();

app.Run();
