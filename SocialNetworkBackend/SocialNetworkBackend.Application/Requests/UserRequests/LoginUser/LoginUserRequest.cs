using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.LoginUser;

public class LoginUserRequest : IRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}