﻿using Microsoft.AspNetCore.Http;
using SocialNetworkBackend.Application.Services;
using System.Security.Claims;

namespace SocialNetworkBackend.Infrastructure.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public Guid? GetUserId() =>
        User is null ? null : (Guid?)Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}