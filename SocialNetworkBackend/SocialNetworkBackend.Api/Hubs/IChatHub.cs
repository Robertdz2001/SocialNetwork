namespace SocialNetworkBackend.Api.Hubs;

public interface IChatHub
{
    Task SendMessageToChat(long chatId);

    Task JoinChat(long chatId);

    Task LeaveChat(long chatId);
}
