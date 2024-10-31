using MediatR;
using Microsoft.AspNetCore.Http;

namespace SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;

public class CreatePostRequest: IRequest
{
    public string Content {  get; set; }

    public IFormFile? Photo { get; set; }
}