using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Infrastructure.Heroes.Persistence
{
    public class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> builder)
        {
            builder.HasKey(hero => hero.Id);
            builder.Property(hero => hero.Id).ValueGeneratedNever();
            builder.Property(hero => hero.Name).HasMaxLength(Hero.MaxNameLength);
        }
    }
}
