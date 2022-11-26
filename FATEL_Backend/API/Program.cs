using System.Text;
using Application;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string secret = builder.Configuration.GetSection("AppSettings")["Secret"];
Byte[] secretBytes = Encoding.ASCII.GetBytes(secret);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

Application.DependencyResolver.DependencyResolverService.RegisterApplicationLayer(builder.Services);
Infrastructure.DependencyResolver.DependencyResolverService.RegisterInfrastructureLayer(builder.Services);
builder.Services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));

//Mapper
var mapper = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<PutItemDTO, Item>();
    configuration.CreateMap<PostItemDTO, Item>();
    configuration.CreateMap<PostWarehouseDTO, Warehouse>();
    configuration.CreateMap<PutWarehouseDTO, Warehouse>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    });

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opts =>
{
    opts.AllowAnyHeader();
    opts.AllowAnyMethod();
    opts.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();