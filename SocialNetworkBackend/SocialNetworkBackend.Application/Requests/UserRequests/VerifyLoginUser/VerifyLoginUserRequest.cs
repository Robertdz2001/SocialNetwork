using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.VerifyLoginUser;

public class VerifyLoginUserRequest : IRequest<VerifyLoginUserDto>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Token { get; set; }
}