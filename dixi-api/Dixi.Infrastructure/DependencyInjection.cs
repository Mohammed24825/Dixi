using Dixi.Application.Common;
using Dixi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dixi.Infrastructure.Auth;

namespace Dixi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DixiDbContext>(opt =>
            opt.UseNpgsql(config.GetConnectionString("DixiDb")));

        services.AddScoped<IDixiDbContext>(provider => provider.GetRequiredService<DixiDbContext>());

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}