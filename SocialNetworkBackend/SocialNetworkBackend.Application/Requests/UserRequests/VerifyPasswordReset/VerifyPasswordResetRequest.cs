using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.VerifyPasswordReset;

public class VerifyPasswordResetRequest : IRequest
{
    public string Email { get; set; }
}