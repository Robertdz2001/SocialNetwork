using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.VerifyRegisterUser;

public class VerifyRegisterUserRequest : IRequest
{
    public string Email { get; set; }
}