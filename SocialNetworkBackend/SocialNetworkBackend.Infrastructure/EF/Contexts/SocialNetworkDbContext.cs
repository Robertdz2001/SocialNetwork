using Microsoft.EntityFrameworkCore;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Infrastructure.EF.Configuration;

namespace SocialNetworkBackend.Infrastructure.EF.Contexts;

public class SocialNetworkDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<VerificationToken> VerificationTokens { get; set; }
    public DbSet<Photo> Photos { get; set; }

    public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new DbContextConfiguration();

        modelBuilder.ApplyConfiguration<User>(configuration);
        modelBuilder.ApplyConfiguration<Role>(configuration);
        modelBuilder.ApplyConfiguration<VerificationToken>(configuration);
        modelBuilder.ApplyConfiguration<Photo>(configuration);
    }
}