﻿namespace SocialNetworkBackend.Domain.Entities;

public class UserComment
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }

    public long PostId { get; set; }

    public Post Post { get; set; }

    public string Content {  get; set; }

    public DateTime Created { get; set; }
}
