using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.GroupRequests.CreateGroup;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupPhoto;
using SocialNetworkBackend.Application.Requests.GroupRequests.GetGroups;

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
}