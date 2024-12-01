using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.GroupRequests.AnswerInviteToGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.CreateGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupInvites;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupPhoto;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroups;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupsForInvite;
using SocialNetworkBackend.Application.Requests.GroupRequests.InviteToGroup;
using SocialNetworkBackend.Application.Requests.UserRequests.GetFriendInvites;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/group")]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] GetGroupsRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{groupId}/photo")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGroupPhoto([FromRoute] long groupId)
    {
        var request = new GetGroupPhotoRequest { GroupId = groupId };

        var result = await _mediator.Send(request);
        return File(result.Data, result.ContentType);
    }

    [HttpGet("groups-for-invite/{userId}")]
    public async Task<IActionResult> GetGroupsForInvite([FromRoute] long userId)
    {
        var result = await _mediator.Send(new GetGroupsForInviteRequest { UserId = userId });
        return Ok(result);
    }

    [HttpPut("{groupId}/invite/{userId}")]
    public async Task<IActionResult> InviteToGroup([FromRoute] long groupId, [FromRoute] long userId)
    {
        await _mediator.Send(new InviteToGroupRequest { UserId = userId, GroupId = groupId });
        return Ok();
    }

    [HttpPut("answer-invite")]
    public async Task<IActionResult> AnswerInviteToGroup(AnswerInviteToGroupRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    [HttpGet("group-invites")]
    [Authorize]
    public async Task<IActionResult> GetGroupInvites([FromQuery] GetGroupInvitesRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}