namespace SocialNetworkBackend.Application.Requests.UserRequests.GetProfilePicture;

public class GetProfilePictureDto
{
    public byte[] Data { get; set; }
    public string ContentType { get; set; }
}