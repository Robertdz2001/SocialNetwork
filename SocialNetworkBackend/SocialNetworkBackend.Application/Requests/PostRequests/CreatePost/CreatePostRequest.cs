using MediatR;
using Microsoft.AspNetCore.Http;

namespace SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;

public class CreatePostRequest: IRequest
{
    public long? GroupId {  get; set; }

    public string Content {  get; set; }

    public IFormFile? Photo { get; set; }
}