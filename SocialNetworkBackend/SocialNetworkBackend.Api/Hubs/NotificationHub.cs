using Microsoft.AspNetCore.SignalR;

namespace SocialNetworkBackend.Api.Hubs;

public class NotificationHub : Hub<INotificationHub>
{
    public async Task JoinNotifications(long userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
    }

    public async Task LeaveNotifications(long userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());
    }
}
