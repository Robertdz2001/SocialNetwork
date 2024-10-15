using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.RegisterUser;

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IVerificationTokenRepository _tokenRepository;
    private readonly IPasswordHasher<VerificationToken> _tokenHasher;

    public RegisterUserRequestHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IVerificationTokenRepository tokenRepository, IPasswordHasher<VerificationToken> tokenHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenRepository = tokenRepository;
        _tokenHasher = tokenHasher;
    }

    public async Task Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var emailAlreadyExists = await _userRepository.GetUserByEmail(request.Email.ToLower());

        if (emailAlreadyExists is not null)
        {
            throw new BadRequestException($"User with email: {request.Email} already exists.");
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

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Country = request.Country,
            City = request.City,
            RoleId = (long)UserRoles.User,
            PasswordHash = ""
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        if (request.ProfilePicture != null)
        {
            using var memoryStream = new MemoryStream();
            await request.ProfilePicture.CopyToAsync(memoryStream);
            var profilePicture = new Photo
            {
                Data = memoryStream.ToArray(),
                ContentType = request.ProfilePicture.ContentType
            };

            user.Photo = profilePicture;
        }

        await _userRepository.AddUser(user);
    }
}