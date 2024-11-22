namespace SocialNetworkBackend.Application.Requests.ChatRequests.GetChatsMessages;

public class GetChatsMessagesDto
{
    public long MessageId {  get; set; }

    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }

    public string UserFirstName {  get; set; }

    public string UserLastName { get; set; }

    public long UserId {  get; set; }

    public bool IsSentByLoggedUser {  get; set; }
}