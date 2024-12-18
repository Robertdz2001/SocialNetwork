﻿using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Repositories;

public interface IPostRepository
{
    Task Create(Post post);

    Task<List<Post>> GetPosts(long loggedUserId);

    Task<List<Post>> GetPostsByUserId(long userId);

    Task<Post?> GetPostById(long postId);

    Task UpdatePost(Post post);

    Task DeletePost(Post post);

    Task<List<Post>> GetPostsByGroupId(long groupId);
}