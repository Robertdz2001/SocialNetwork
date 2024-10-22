﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialNetworkBackend.Infrastructure.EF.Contexts;

#nullable disable

namespace SocialNetworkBackend.Infrastructure.EF.Migrations
{
    [DbContext(typeof(SocialNetworkDbContext))]
    partial class SocialNetworkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.FriendInvite", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendInvites");
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<long?>("PhotoId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            City = "City1",
                            Country = "Country1",
                            Email = "user1@gmail.com",
                            FirstName = "FirstName1",
                            LastName = "LastName1",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+1543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            City = "City2",
                            Country = "Country2",
                            Email = "user2@gmail.com",
                            FirstName = "FirstName2",
                            LastName = "LastName2",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+2543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            City = "City3",
                            Country = "Country3",
                            Email = "user3@gmail.com",
                            FirstName = "FirstName3",
                            LastName = "LastName3",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+3543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 4L,
                            City = "City4",
                            Country = "Country4",
                            Email = "user4@gmail.com",
                            FirstName = "FirstName4",
                            LastName = "LastName4",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+4543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 5L,
                            City = "City5",
                            Country = "Country5",
                            Email = "user5@gmail.com",
                            FirstName = "FirstName5",
                            LastName = "LastName5",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+5543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 6L,
                            City = "City6",
                            Country = "Country6",
                            Email = "user6@gmail.com",
                            FirstName = "FirstName6",
                            LastName = "LastName6",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+6543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 7L,
                            City = "City7",
                            Country = "Country7",
                            Email = "user7@gmail.com",
                            FirstName = "FirstName7",
                            LastName = "LastName7",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+7543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 8L,
                            City = "City8",
                            Country = "Country8",
                            Email = "user8@gmail.com",
                            FirstName = "FirstName8",
                            LastName = "LastName8",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+8543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 9L,
                            City = "City9",
                            Country = "Country9",
                            Email = "user9@gmail.com",
                            FirstName = "FirstName9",
                            LastName = "LastName9",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+9543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 10L,
                            City = "City10",
                            Country = "Country10",
                            Email = "user10@gmail.com",
                            FirstName = "FirstName10",
                            LastName = "LastName10",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+10543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 11L,
                            City = "City11",
                            Country = "Country11",
                            Email = "user11@gmail.com",
                            FirstName = "FirstName11",
                            LastName = "LastName11",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+11543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 12L,
                            City = "City12",
                            Country = "Country12",
                            Email = "user12@gmail.com",
                            FirstName = "FirstName12",
                            LastName = "LastName12",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+12543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 13L,
                            City = "City13",
                            Country = "Country13",
                            Email = "user13@gmail.com",
                            FirstName = "FirstName13",
                            LastName = "LastName13",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+13543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 14L,
                            City = "City14",
                            Country = "Country14",
                            Email = "user14@gmail.com",
                            FirstName = "FirstName14",
                            LastName = "LastName14",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+14543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 15L,
                            City = "City15",
                            Country = "Country15",
                            Email = "user15@gmail.com",
                            FirstName = "FirstName15",
                            LastName = "LastName15",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+15543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 16L,
                            City = "City16",
                            Country = "Country16",
                            Email = "user16@gmail.com",
                            FirstName = "FirstName16",
                            LastName = "LastName16",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+16543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 17L,
                            City = "City17",
                            Country = "Country17",
                            Email = "user17@gmail.com",
                            FirstName = "FirstName17",
                            LastName = "LastName17",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+17543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 18L,
                            City = "City18",
                            Country = "Country18",
                            Email = "user18@gmail.com",
                            FirstName = "FirstName18",
                            LastName = "LastName18",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+18543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 19L,
                            City = "City19",
                            Country = "Country19",
                            Email = "user19@gmail.com",
                            FirstName = "FirstName19",
                            LastName = "LastName19",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+19543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 20L,
                            City = "City20",
                            Country = "Country20",
                            Email = "user20@gmail.com",
                            FirstName = "FirstName20",
                            LastName = "LastName20",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+20543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 21L,
                            City = "City21",
                            Country = "Country21",
                            Email = "user21@gmail.com",
                            FirstName = "FirstName21",
                            LastName = "LastName21",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+21543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 22L,
                            City = "City22",
                            Country = "Country22",
                            Email = "user22@gmail.com",
                            FirstName = "FirstName22",
                            LastName = "LastName22",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+22543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 23L,
                            City = "City23",
                            Country = "Country23",
                            Email = "user23@gmail.com",
                            FirstName = "FirstName23",
                            LastName = "LastName23",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+23543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 24L,
                            City = "City24",
                            Country = "Country24",
                            Email = "user24@gmail.com",
                            FirstName = "FirstName24",
                            LastName = "LastName24",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+24543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 25L,
                            City = "City25",
                            Country = "Country25",
                            Email = "user25@gmail.com",
                            FirstName = "FirstName25",
                            LastName = "LastName25",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+25543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 26L,
                            City = "City26",
                            Country = "Country26",
                            Email = "user26@gmail.com",
                            FirstName = "FirstName26",
                            LastName = "LastName26",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+26543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 27L,
                            City = "City27",
                            Country = "Country27",
                            Email = "user27@gmail.com",
                            FirstName = "FirstName27",
                            LastName = "LastName27",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+27543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 28L,
                            City = "City28",
                            Country = "Country28",
                            Email = "user28@gmail.com",
                            FirstName = "FirstName28",
                            LastName = "LastName28",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+28543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 29L,
                            City = "City29",
                            Country = "Country29",
                            Email = "user29@gmail.com",
                            FirstName = "FirstName29",
                            LastName = "LastName29",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+29543654753",
                            RoleId = 1L
                        },
                        new
                        {
                            Id = 30L,
                            City = "City30",
                            Country = "Country30",
                            Email = "user30@gmail.com",
                            FirstName = "FirstName30",
                            LastName = "LastName30",
                            PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==",
                            PhoneNumber = "+30543654753",
                            RoleId = 1L
                        });
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.VerificationToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TokenHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("VerificationTokens");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.Property<long>("FriendsId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("FriendsId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserUser");
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.FriendInvite", b =>
                {
                    b.HasOne("SocialNetworkBackend.Domain.Entities.User", "Receiver")
                        .WithMany("ReceivedFriendInvites")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialNetworkBackend.Domain.Entities.User", "Sender")
                        .WithMany("SentFriendInvites")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.Photo", b =>
                {
                    b.HasOne("SocialNetworkBackend.Domain.Entities.User", "User")
                        .WithOne("Photo")
                        .HasForeignKey("SocialNetworkBackend.Domain.Entities.Photo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.User", b =>
                {
                    b.HasOne("SocialNetworkBackend.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UserUser", b =>
                {
                    b.HasOne("SocialNetworkBackend.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("FriendsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialNetworkBackend.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SocialNetworkBackend.Domain.Entities.User", b =>
                {
                    b.Navigation("Photo");

                    b.Navigation("ReceivedFriendInvites");

                    b.Navigation("SentFriendInvites");
                });
#pragma warning restore 612, 618
        }
    }
}
