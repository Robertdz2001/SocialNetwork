using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.UserRequests.AddFriend;
using SocialNetworkBackend.Application.Requests.UserRequests.AnswerFriendInvite;
using SocialNetworkBackend.Application.Requests.UserRequests.DeleteFriend;
using SocialNetworkBackend.Application.Requests.UserRequests.GetFriendInvites;
using SocialNetworkBackend.Application.Requests.UserRequests.GetMutualFriends;
using SocialNetworkBackend.Application.Requests.UserRequests.GetProfilePicture;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUserDetails;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUserShortInfo;
using SocialNetworkBackend.Application.Requests.UserRequests.LoginUser;
using SocialNetworkBackend.Application.Requests.UserRequests.PasswordReset;
using SocialNetworkBackend.Application.Requests.UserRequests.RegisterUser;
using SocialNetworkBackend.Application.Requests.UserRequests.VerifyLoginUser;
using SocialNetworkBackend.Application.Requests.UserRequests.VerifyPasswordReset;
using SocialNetworkBackend.Application.Requests.UserRequests.VerifyRegisterUser;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Checks if email is not taken and sends verification token.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("verify-register")]
    public async Task<IActionResult> VerifyRegisterUser(VerifyRegisterUserRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Creates new user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
    {
        var validator = new RegisterUserRequestValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Checks email and passwords then sends verification code.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Checks token during logging in.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("verify-login")]
    public async Task<IActionResult> VerifyLoginUser(VerifyLoginUserRequest request)
    {
        var token = await _mediator.Send(request);
        return Ok(token);
    }

    /// <summary>
    /// Checks token during resetting password.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("verify-password-reset")]
    public async Task<IActionResult> VerifyPasswordReset(VerifyPasswordResetRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Resets password.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset(PasswordResetRequest request)
    {
        var validator = new PasswordResetRequestValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Gets users profile picture.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("{userId}/profile-picture")]
    public async Task<IActionResult> GetProfilePicture([FromRoute] long userId)
    {
        var request = new GetProfilePictureRequest { UserId = userId };

        var result = await _mediator.Send(request);
        return File(result.Data, result.ContentType);
    }

    /// <summary>
    /// Checks if user is logged in.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("is-logged-in")]
    public IActionResult IsLoggedIn()
    {
        return Ok();
    }

    [Authorize]
    [HttpGet("user-short-info")]
    public async Task<IActionResult> GetUserShortInfo()
    {
        var result = await _mediator.Send(new GetUserShortInfoRequest());
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("{id}/add-friend")]
    [Authorize]
    public async Task<IActionResult> AddFriend([FromRoute] long id)
    {
        await _mediator.Send(new AddFriendRequest { UserId = id});
        return Ok();
    }

    [HttpGet("friend-invites")]
    [Authorize]
    public async Task<IActionResult> GetFriendInvites([FromQuery] GetFriendInvitesRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpPut("answer-friend-invite")]
    [Authorize]
    public async Task<IActionResult> AnswerFriendInvite(AnswerFriendInviteRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPut("{userId}/delete-friend")]
    [Authorize]
    public async Task<IActionResult> DeleteFriend([FromRoute] long userId)
    {
        await _mediator.Send(new DeleteFriendRequest {UserId = userId});
        return Ok();
    }

    [HttpGet("mutual-friends")]
    [Authorize]
    public async Task<IActionResult> GetMutualFriends([FromQuery] GetMutualFriendsRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserDetails([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetUserDetailsRequest { Id = id });
        return Ok(result);
    }
}