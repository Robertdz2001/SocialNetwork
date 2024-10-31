using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Infrastructure.EF.Contexts;
using SocialNetworkBackend.Infrastructure.EF.Options;
using SocialNetworkBackend.Infrastructure.EF.Repositories;
using SocialNetworkBackend.Shared.Options;

namespace SocialNetworkBackend.Infrastructure.EF;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {

        var options = configuration.GetOptions<PostgresOptions>("Postgres");

        services.AddDbContext<SocialNetworkDbContext>(ctx
            => ctx.UseNpgsql(options.ConnectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVerificationTokenRepository, VerificationTokenRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        return services;
    }
}