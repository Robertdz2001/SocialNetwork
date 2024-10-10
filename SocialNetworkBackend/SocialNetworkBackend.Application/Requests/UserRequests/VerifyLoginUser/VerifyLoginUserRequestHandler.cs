using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.VerifyLoginUser;

public class VerifyLoginUserRequestHandler : IRequestHandler<VerifyLoginUserRequest, VerifyLoginUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IVerificationTokenRepository _tokenRepository;
    private readonly IPasswordHasher<VerificationToken> _tokenHasher;
    private readonly IJwtService _jwtService;

    public VerifyLoginUserRequestHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IVerificationTokenRepository tokenRepository, IPasswordHasher<VerificationToken> tokenHasher, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenRepository = tokenRepository;
        _tokenHasher = tokenHasher;
        _jwtService = jwtService;
    }

    public async Task<VerifyLoginUserDto> Handle(VerifyLoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email.ToLower())
            ?? throw new BadRequestException($"Invalid email or password"); 

        var isPasswordCorrect = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (isPasswordCorrect == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException($"Invalid email or password");
        }

        var token = await _tokenRepository.GetTokenByUserEmail(request.Email.ToLower())
            ?? throw new NotFoundException("Tokens not found");

        var result = _tokenHasher.VerifyHashedPassword(token, token.TokenHash, request.Token);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid token");
        }

        if (token.ValidTo < DateTime.UtcNow)
        {
            throw new BadRequestException("Token has expired");
        }

        var jwtToken = _jwtService.GetJwtToken(user);

        return new VerifyLoginUserDto()
        {
            Token = jwtToken
        };
    }
}