using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Infrastructure.EF;
using SocialNetworkBackend.Infrastructure.EF.Options;
using SocialNetworkBackend.Infrastructure.Jwt;
using SocialNetworkBackend.Infrastructure.Services;
using SocialNetworkBackend.Shared.Options;

namespace SocialNetworkBackend.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<SmtpOptions>("Smtp");
        services.AddSingleton(options);

        services.AddHttpContextAccessor();
        services.AddPostgres(configuration);
        services.AddJwt(configuration);
        services.AddCors(options => {
            options.AddPolicy("FrontEndClient", builder => {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
        });
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ISmtpService, SmtpService>();
        services.AddScoped<IUserContextService, UserContextService>();

        return services;
    }
}