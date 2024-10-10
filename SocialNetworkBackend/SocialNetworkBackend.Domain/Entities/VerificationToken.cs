namespace SocialNetworkBackend.Domain.Entities;

/// <summary>
/// Used in authentication.
/// </summary>
public class VerificationToken
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string TokenHash { get; set; }

    public DateTime Created { get; set; }
    public DateTime ValidTo { get; set; }
}