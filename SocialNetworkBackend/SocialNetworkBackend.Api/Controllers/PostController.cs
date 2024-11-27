using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.PostRequests.AddComment;
using SocialNetworkBackend.Application.Requests.PostRequests.CreatePost;
using SocialNetworkBackend.Application.Requests.PostRequests.DeleteComment;
using SocialNetworkBackend.Application.Requests.PostRequests.DeletePost;
using SocialNetworkBackend.Application.Requests.PostRequests.GetComments;
using SocialNetworkBackend.Application.Requests.PostRequests.GetPostPhoto;
using SocialNetworkBackend.Application.Requests.PostRequests.GetPosts;
using SocialNetworkBackend.Application.Requests.PostRequests.GetUsersPosts;
using SocialNetworkBackend.Application.Requests.PostRequests.TogglePostLike;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Authorize]
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

    /// <summary>
    /// Toggles post like. If user already liked post it will delete this like.
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpPut("{postId}/toggle-like")]
    public async Task<IActionResult> TogglePostLike([FromRoute] long postId)
    {
        var request = new TogglePostLikeRequest { PostId = postId };

        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost("{postId}/comment")]
    public async Task<IActionResult> AddComment([FromRoute] long postId, [FromBody] string content)
    {
        var request = new AddCommentRequest { PostId = postId, Content = content };

        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet("{postId}/comment")]
    public async Task<IActionResult> GetComments([FromRoute] long postId)
    {
        var request = new GetCommentsRequest { PostId = postId };

        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUsersPosts([FromRoute] long userId)
    {
        var result = await _mediator.Send(new GetUsersPostsRequest { UserId = userId } );
        return Ok(result);
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost([FromRoute] long postId)
    {
        var request = new DeletePostRequest { PostId = postId };

        await _mediator.Send(request);
        return Ok();
    }

    [HttpDelete("{postId}/comments/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] long postId, [FromRoute] long commentId)
    {
        var request = new DeleteCommentRequest { PostId = postId, CommentId = commentId };

        await _mediator.Send(request);
        return Ok();
    }
}
