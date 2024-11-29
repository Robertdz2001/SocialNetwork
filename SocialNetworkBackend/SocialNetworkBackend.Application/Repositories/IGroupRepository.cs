using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IGroupRepository
{
    Task Create(Group group);

    Task<List<Group>> GetGroups();

    Task<Group?> GetGroupById(long groupId);
}