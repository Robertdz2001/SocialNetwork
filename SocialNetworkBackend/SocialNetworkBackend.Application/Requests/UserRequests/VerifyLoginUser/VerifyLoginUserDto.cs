namespace SocialNetworkBackend.Application.Requests.UserRequests.VerifyLoginUser;

public class VerifyLoginUserDto
{
    /// <summary>
    /// Jwt token used for authorization.
    /// </summary>
    public string Token { get; set; }
}