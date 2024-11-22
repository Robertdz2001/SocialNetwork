using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.AnswerFriendInvite;

public class AnswerFriendInviteRequestHandler : IRequestHandler<AnswerFriendInviteRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public AnswerFriendInviteRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(AnswerFriendInviteRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User not found");

        var userToAdd = await _userRepository.GetUserById(request.UserId)
            ?? throw new NotFoundException("User not found");

        var friendInviteToRemove = userToAdd.SentFriendInvites.FirstOrDefault(x => x.ReceiverId == loggedUserId)
            ?? throw new NotFoundException("Friend invite not found");

        userToAdd.SentFriendInvites.Remove(friendInviteToRemove);

        if (request.IsAccepted)
        {
            var chat = new Chat()
            {
                User1Id = loggedUserId,
                User2Id = userToAdd.Id,
            };

            userToAdd.Friends.Add(loggedUser);
            loggedUser.Friends.Add(userToAdd);
            loggedUser.ChatsAsUser1.Add(chat);
        }

        await _userRepository.Update(userToAdd);
        await _userRepository.Update(loggedUser);
    }
}