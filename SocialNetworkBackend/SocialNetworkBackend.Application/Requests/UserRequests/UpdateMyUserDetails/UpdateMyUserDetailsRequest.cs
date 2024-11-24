using MediatR;
using Microsoft.AspNetCore.Http;

namespace SocialNetworkBackend.Application.Requests.UserRequests.UpdateMyUserDetails;

public class UpdateMyUserDetailsRequest : IRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public IFormFile? ProfilePicture { get; set; }
}