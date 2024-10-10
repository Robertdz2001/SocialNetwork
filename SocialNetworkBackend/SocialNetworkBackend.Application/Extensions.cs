using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkBackend.Domain.Entities;
using System.Reflection;

namespace SocialNetworkBackend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}