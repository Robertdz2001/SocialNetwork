using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialNetworkBackend.Infrastructure.EF.Options;
using System.Text;
using SocialNetworkBackend.Shared.Options;

namespace SocialNetworkBackend.Infrastructure.Jwt;

public static class Extensions
{
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AuthenticationSettings>("Authentication");

        services.AddSingleton(options);
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = options.JwtIssuer,
                ValidAudience = options.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.JwtKey)),

            };
        });

        return services;
    }
}