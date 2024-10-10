using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.RegisterUser;

public class RegisterUserRequest : IRequest
{
    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Token { get; set; }

    public required string Password { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }
}