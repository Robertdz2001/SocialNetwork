using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IChatRepository
{
    Task DeleteByUsersId(long user1Id, long user2Id);

    Task<Chat?> GetChatById(long chatId);

    Task Update(Chat chat);
}