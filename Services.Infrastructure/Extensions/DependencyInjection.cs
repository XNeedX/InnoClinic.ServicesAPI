using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.Application.Abstractions;
using Services.Application.Behaviours;
using Services.Domain.Models;
using Services.Infrastructure.Data;
using Services.Infrastructure.Options;
using Services.Infrastructure.Repositories;

namespace Services.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(
           configuration.GetSection(nameof(ConnectionStrings)));

        services.AddDbContext<ServicesDbContext>((sp, options) =>
        {
            var connectionOptions = sp.GetRequiredService<IOptions<ConnectionStrings>>().Value;
            options.UseSqlServer(connectionOptions.DefaultConnection);
        });

        services.AddScoped<ServicesDbContext>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelimeBehaviour<,>));

        services.AddValidatorsFromAssembly(typeof(Services.Application.Extensions.DependencyInjection).Assembly,
            includeInternalTypes: true);

        return services;
    }
}
