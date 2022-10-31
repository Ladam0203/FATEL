using Application.DTOs;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

Application.DependencyResolver.DependencyResolverService.RegisterApplicationLayer(builder.Services);
Infrastructure.DependencyResolver.DependencyResolverService.RegisterInfrastructureLayer(builder.Services);

//Mapper
var mapper = new MapperConfiguration(configuration =>
{
    configuration.CreateMap<PostItemDTO, Item>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();