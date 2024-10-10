using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkBackend.Infrastructure.EF.Contexts;
using SocialNetworkBackend.Infrastructure.EF.Options;
using SocialNetworkBackend.Shared.Options;

namespace SocialNetworkBackend.Infrastructure.EF;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {

        var options = configuration.GetOptions<PostgresOptions>("Postgres");

        services.AddDbContext<SocialNetworkDbContext>(ctx
            => ctx.UseNpgsql(options.ConnectionString));

        //services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}