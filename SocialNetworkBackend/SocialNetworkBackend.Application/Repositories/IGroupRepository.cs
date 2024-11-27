using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IGroupRepository
{
    Task Create(Group group);

    Task<List<Group>> GetGroups();
}