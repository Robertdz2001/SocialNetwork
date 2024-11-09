using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;
using SocialNetworkBackend.Application.Requests.PostRequests.GetPostPhoto;
using SocialNetworkBackend.Application.Requests.PostRequests.GetPosts;

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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPosts([FromQuery] GetPostsRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{postId}/photo")]
    public async Task<IActionResult> GetPostPhoto([FromRoute] long postId)
    {
        var request = new GetPostPhotoRequest { PostId = postId };

        var result = await _mediator.Send(request);
        return File(result.Data, result.ContentType);
    }
}
