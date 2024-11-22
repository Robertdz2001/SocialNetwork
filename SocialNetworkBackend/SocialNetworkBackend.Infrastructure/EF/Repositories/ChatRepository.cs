using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Contexts;

namespace SocialNetworkBackend.Infrastructure.EF.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly SocialNetworkDbContext _dbContext;

    public ChatRepository(SocialNetworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteByUsersId(long user1Id, long user2Id)
    {
        var chatToDelete = await _dbContext.Chats
            .FirstOrDefaultAsync(x => (x.User1Id == user1Id && x.User2Id == user2Id) || (x.User1Id == user2Id && x.User2Id == user1Id));

        if (chatToDelete != null)
        {
            _dbContext.Chats.Remove(chatToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Chat?> GetChatById(long chatId)
        => await _dbContext.Chats
            .Include(x => x.Messages)
            .ThenInclude(y => y.User)
            .FirstOrDefaultAsync(x => x.Id == chatId);

    public async Task Update(Chat chat)
    {
        _dbContext.Update(chat);
        await _dbContext.SaveChangesAsync();
    }
}