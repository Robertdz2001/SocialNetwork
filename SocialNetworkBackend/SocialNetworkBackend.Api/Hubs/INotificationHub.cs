namespace SocialNetworkBackend.Api.Hubs;

public interface INotificationHub
{
    Task ReceiveFriendRequest();
    Task ReceiveGroupRequest();
}
