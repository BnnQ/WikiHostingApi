using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WikiHostingApi.Entities;

namespace WikiHostingApi.Extensions;

public static class DatabaseSeedExtensions
{
    private const string StubUserId = "ac8cca8b-8b9c-43fe-9ed1-999edc438751";
    private const string StubAdminUserId = "b74ddd14-6340-4840-95c2-db12554843e5";

    public static ModelBuilder SeedUsers(this ModelBuilder builder)
    {
        var stubUser = new User
        {
            Id = StubUserId,
            UserName = "user",
            NormalizedUserName = "USER",
            Email = "user@gmail.com",
            NormalizedEmail = "USER@gmail.com",
            LockoutEnabled = false,
            PhoneNumber = "+380687304144"
        };

        var stubAdminUser = new User
        {
            Id = StubAdminUserId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@gmail",
            LockoutEnabled = false,
            PhoneNumber = "+380687507133"
        };

        var passwordHasher = new PasswordHasher<User>();
        stubUser.PasswordHash = passwordHasher.HashPassword(stubUser, "1111");
        stubAdminUser.PasswordHash = passwordHasher.HashPassword(stubAdminUser, "1111");

        builder.Entity<User>().HasData(stubUser, stubAdminUser);
        return builder;
    }

    private const string UserRoleId = "8d04dce2-969a-435d-bba4-8d125ecd5630";
    private const string ModeratorRoleId = "fab4fac1-c546-41de-aebc-a14da6895711";
    private const string AdminRoleId = "c7b013f0-5201-4317-abd8-c211f91b7330";

    public static ModelBuilder SeedRoles(this ModelBuilder builder)
    {
        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Id = UserRoleId, Name = "User", ConcurrencyStamp = "1",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = ModeratorRoleId, Name = "Moderator", ConcurrencyStamp = "1",
                    NormalizedName = "MODERATOR"
                },
                new IdentityRole
                {
                    Id = AdminRoleId, Name = "Admin", ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN"
                });

        return builder;
    }

    public static ModelBuilder SeedUserRoles(this ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>()
            .HasData(
                new IdentityUserRole<string> { RoleId = UserRoleId, UserId = StubUserId },
                new IdentityUserRole<string> { RoleId = AdminRoleId, UserId = StubAdminUserId });

        return builder;
    }

    public static ModelBuilder SeedContributorRoles(this ModelBuilder builder)
    {
        builder.Entity<ContributorRole>()
            .HasData(
                new ContributorRole
                {
                    Id = 1,
                    Name = "Owner"
                },
                new ContributorRole
                {
                    Id = 2,
                    Name = "Admin"
                },
                new ContributorRole
                {
                    Id = 3,
                    Name = "Moderator"
                },
                new ContributorRole
                {
                    Id = 4,
                    Name = "Contributor"
                });

        return builder;
    }

    public static ModelBuilder SeedSubscriptions(this ModelBuilder builder)
    {
        builder.Entity<Subscription>()
            .HasData(
                new Subscription
                {
                    Id = 1,
                    Name = "Free",
                    Price = 0,
                    Description =
                        "<ul><li>Ideal for individuals and small teams starting their wiki journey.</li><li>Access to basic wiki creation and editing tools.</li><li>Limited storage space.</li><li>Community support.</li></ul>",
                    DurationDays = 500000
                },
                new Subscription
                {
                    Id = 2,
                    Name = "Basic",
                    Price = 5,
                    Description =
                        "<ul><li>Perfect for growing teams that need more advanced features and storage.</li><li>Full access to wiki creation and editing tools.</li><li>Increased storage space.</li><li>Priority email support.</li><li>Access to version history.</li></ul>",
                    DurationDays = 30
                },
                new Subscription
                {
                    Id = 3,
                    Name = "Premium",
                    Price = 10,
                    Description =
                        "<ul><li>Designed for large organizations and enterprises with extensive wiki needs.</li><li>Unlimited access to all wiki creation and editing tools.</li><li>Unlimited storage space.</li><li>24/7 priority support.</li><li>Access to version history and advanced analytics.</li><li>Custom branding options.</li></ul>",
                    DurationDays = 30
                });

        return builder;
    }

    public static ModelBuilder SeedTopics(this ModelBuilder builder)
    {
        builder.Entity<Topic>()
            .HasData(new Topic
                {
                    Id = 1,
                    Name = "Games"
                },
                new Topic
                {
                    Id = 2,
                    Name = "Movies"
                },
                new Topic
                {
                    Id = 3,
                    Name = "TV"
                },
                new Topic
                {
                    Id = 4,
                    Name = "Anime"
                });

        return builder;
    }
}