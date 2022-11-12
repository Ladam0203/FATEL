using Application.Interfaces;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Test;

namespace Infrastructure.DependencyResolver;

public static class DependencyResolverService
{
    public static void RegisterInfrastructureLayer(IServiceCollection services)
    {
        services.AddScoped<IRepositoryFacade, RepositoryFacade>();
    }
}