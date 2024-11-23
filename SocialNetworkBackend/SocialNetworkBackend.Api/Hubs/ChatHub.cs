using Microsoft.AspNetCore.SignalR;

namespace SocialNetworkBackend.Api.Hubs;

public class ChatHub : Hub<IChatHub>
{
    public async Task JoinChat(long chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task LeaveChat(long chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
}
