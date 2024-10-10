﻿using Microsoft.IdentityModel.Tokens;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialNetworkBackend.Infrastructure.Services;

/// <summary>
/// Jwt service.
/// </summary>
public class JwtService : IJwtService
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtService(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }

    ///<inheritdoc/>
    public string GetJwtToken(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, $"{user.Email}"),
            new(ClaimTypes.Role, $"{user.Role.Name}")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);


        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}