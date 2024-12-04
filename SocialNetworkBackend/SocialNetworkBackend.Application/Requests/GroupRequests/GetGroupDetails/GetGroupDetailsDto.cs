namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupDetails;

public class GetGroupDetailsDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public List<MemberDto> Members { get; set; } = new();

    public bool CanDelete { get; set; }

    public bool CanCreatePost {  get; set; }

    public class MemberDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool CanDelete { get; set; }
    }
}