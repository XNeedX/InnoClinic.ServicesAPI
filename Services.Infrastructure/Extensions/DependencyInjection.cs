using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.Infrastructure.Data;
using Services.Domain.Models;
using Services.Infrastructure.Options;

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

        return services;
    }
}
