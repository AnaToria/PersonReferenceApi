using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PersonReferenceDbContext>(builder 
            => builder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}