﻿using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetPosts;

public class GetPostsRequestHandler : IRequestHandler<GetPostsRequest, PagedResult<GetPostsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public GetPostsRequestHandler(IPostRepository postRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task<PagedResult<GetPostsDto>> Handle(GetPostsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var posts = await _postRepository.GetPosts(loggedUserId);

        var pageSize = 5;

        if (!string.IsNullOrEmpty(request.CreatedUserFirstName))
        {
            posts = posts.Where(p => p.CreatedUser.FirstName.Contains(request.CreatedUserFirstName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(request.CreatedUserLastName))
        {
            posts = posts.Where(p => p.CreatedUser.LastName.Contains(request.CreatedUserLastName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(request.Content))
        {
            posts = posts.Where(p => p.Content.Contains(request.Content, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var postsDto = posts.Select(x => new GetPostsDto
        {
            CreatedUserId = x.CreatedUserId,
            CreatedUserFirstName = x.CreatedUser.FirstName,
            CreatedUserLastName = x.CreatedUser.LastName,
            PostId = x.Id,
            Content = x.Content,
            CreatedDate = x.Created,
            UserLikesCount = x.UserLikes.Count,
            IsLiked = x.UserLikes.FirstOrDefault(y => y.UserId == loggedUserId) is not null,
            CommentsCount = x.UserComments.Count,
            CanDelete = x.CreatedUserId == loggedUserId || loggedUser.RoleId == (long)UserRoles.Admin
        }).OrderByDescending(x => x.CreatedDate)
        .ToList();

        var pagedResult = new PagedResult<GetPostsDto>(postsDto, postsDto.Count, pageSize, request.PageNumber);

        return pagedResult;
    }
}