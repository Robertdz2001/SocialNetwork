using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.ChatRequests.GetChats;
using SocialNetworkBackend.Application.Requests.ChatRequests.GetChatsMessages;
using SocialNetworkBackend.Application.Requests.ChatRequests.SendMessage;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
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
        return Ok();
    }
}
