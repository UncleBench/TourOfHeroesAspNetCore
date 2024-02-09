using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourOfHeroes.Domain.Heroes;
using TourOfHeroes.Domain.Heroes.Entities;
using TourOfHeroes.Domain.Heroes.ValueObjects;

namespace TourOfHeroes.Infrastructure.Heroes.Persistence
{
    internal sealed class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            ConfigureHeroesTable(builder);
            ConfigureSuperPowersTable(builder);
        }

        private void ConfigureSuperPowersTable(EntityTypeBuilder<Hero> heroBuilder)
        {
            heroBuilder.OwnsMany(hero => hero.SuperPowers, superPowerBuilder =>
            {
                superPowerBuilder.ToTable("SuperPowers");
                superPowerBuilder.WithOwner().HasForeignKey(nameof(HeroId));
                superPowerBuilder.HasKey(nameof(SuperPower.Id), nameof(HeroId));

                superPowerBuilder.Property(superPower => superPower.Id)
                    .HasColumnName(nameof(SuperPowerId))
                    .HasConversion(
                        id => id.Value,
                        value => SuperPowerId.Create(value));

                superPowerBuilder.Property(superPower => superPower.Name)
                    .HasMaxLength(SuperPower.MaxNameLength);

                superPowerBuilder.Property(superPower => superPower.Description)
                    .HasMaxLength(SuperPower.MaxDescriptionLength);
            });
        }

        private void ConfigureHeroesTable(EntityTypeBuilder<Hero> heroBuilder)
        {
            heroBuilder.ToTable("Heroes");
            heroBuilder.HasKey(hero => hero.Id);

            heroBuilder.Property(hero => hero.Id)
                .HasConversion(
                    id => id.Value,
                    value => HeroId.Create(value));

            heroBuilder.Property(hero => hero.Name)
                .HasMaxLength(Hero.MaxNameLength);
        }
    }
}
