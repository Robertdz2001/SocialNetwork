using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;
using SocialNetworkBackend.Application.Requests.UserRequests.RegisterUser;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates new post.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }
}
