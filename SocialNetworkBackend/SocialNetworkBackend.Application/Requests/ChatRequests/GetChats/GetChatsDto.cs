namespace SocialNetworkBackend.Application.Requests.ChatRequests.GetChats;

public class GetChatsDto
{
    public long Id { get; set; }

    public string UserFirstName {  get; set; }

    public string UserLastName { get; set; }

    public long UserId {  get; set; }
}