using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourOfHeroes.Domain.Users;
using TourOfHeroes.Domain.Users.ValueObjects;

namespace TourOfHeroes.Infrastructure.Users.Persistence
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureHeroesTable(builder);
        }

        private void ConfigureHeroesTable(EntityTypeBuilder<User> userBuilder)
        {
            userBuilder.ToTable("Users");
            userBuilder.HasKey(user => user.Id);

            userBuilder.Property(user => user.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));

            userBuilder.Property(x => x.FirstName);
            userBuilder.Property(x => x.LastName);
            userBuilder.Property(x => x.Email);
            userBuilder.Property(x => x.Password);
        }
    }
}
