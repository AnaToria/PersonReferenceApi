using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblyFullName = typeof(PersonReferenceDbContext).Assembly.FullName;
        services.AddDbContext<PersonReferenceDbContext>(builder =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly(assemblyFullName);
                });
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IImageService, ImageService>();
        services.AddSingleton<IStringLocalizer, StringLocalizer>();
        
        return services;
     }
}