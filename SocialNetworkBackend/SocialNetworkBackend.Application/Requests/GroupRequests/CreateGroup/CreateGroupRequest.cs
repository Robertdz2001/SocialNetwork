using MediatR;
using Microsoft.AspNetCore.Http;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.CreateGroup;

public class CreateGroupRequest : IRequest
{
    public string Name { get; set; }

    public string Description {  get; set; }

    public IFormFile? Photo { get; set; }
}