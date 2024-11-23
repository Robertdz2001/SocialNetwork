using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialNetworkBackend.Api.Hubs;
using SocialNetworkBackend.Application.Requests.ChatRequests.GetChats;
using SocialNetworkBackend.Application.Requests.ChatRequests.GetChatsMessages;
using SocialNetworkBackend.Application.Requests.ChatRequests.SendMessage;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<ChatHub, IChatHub> _hub;

    public ChatController(IMediator mediator, IHubContext<ChatHub, IChatHub> hub)
    {
        _mediator = mediator;
        _hub = hub;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetChats()
    {
        var result = await _mediator.Send(new GetChatsRequest());
        return Ok(result);
    }

    [HttpGet("{id}/messages")]
    [Authorize]
    public async Task<IActionResult> GetChatsMessages([FromRoute] long id)
    {
        var result = await _mediator.Send(new GetChatsMessagesRequest { Id = id });
        return Ok(result);
    }

    [HttpPost("{id}/messages")]
    [Authorize]
    public async Task<IActionResult> SendMessage([FromRoute] long id, [FromBody] string content)
    {
        await _mediator.Send(new SendMessageRequest { Id = id, Content = content });

        await _hub.Clients.Groups(id.ToString()).SendMessageToChat(id);

        return Ok();
    }
}
