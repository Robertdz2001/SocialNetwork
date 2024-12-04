using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialNetworkBackend.Api.Hubs;
using SocialNetworkBackend.Application.Requests.GroupRequests.AnswerInviteToGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.CreateGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.DeleteGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.DeleteMemberFromGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupDetails;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupInvites;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupPhoto;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroups;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupsForInvite;
using SocialNetworkBackend.Application.Requests.GroupRequests.InviteToGroup;
using SocialNetworkBackend.Application.Requests.UserRequests.GetUserDetails;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/group")]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<NotificationHub, INotificationHub> _hub;

    public GroupController(IMediator mediator, IHubContext<NotificationHub, INotificationHub> hub)
    {
        _mediator = mediator;
        _hub = hub;
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
        await _hub.Clients.Group(userId.ToString()).ReceiveGroupRequest();
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGroupDetails([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetGroupDetailsRequest { Id = id });
        return Ok(result);
    }

    [HttpDelete("{groupId}")]
    public async Task<IActionResult> DeleteGroup([FromRoute] long groupId)
    {
        await _mediator.Send(new DeleteGroupRequest { Id = groupId });
        return Ok();
    }

    [HttpDelete("{groupId}/delete-member/{memberId}")]
    public async Task<IActionResult> DeleteMemberFromGroup([FromRoute] long groupId, [FromRoute] long memberId)
    {
        await _mediator.Send(new DeleteMemberFromGroupRequest { GroupId = groupId, MemberId = memberId });
        return Ok();
    }
}