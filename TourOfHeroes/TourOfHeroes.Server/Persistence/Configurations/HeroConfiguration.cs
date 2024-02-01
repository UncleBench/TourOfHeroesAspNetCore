using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourOfHeroes.Server.Models;

namespace TourOfHeroes.Server.Persistence.Configurations
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
