using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.PasswordReset;

public class PasswordResetRequest: IRequest
{
    public required string Email { get; set; }

    public required string Token { get; set; }

    public required string Password { get; set; }
}