using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Infrastructure.Common.Persistence
{
    public sealed class TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> options) : DbContext(options)
    {
        public DbSet<Hero> Heroes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TourOfHeroesDbContext).Assembly);
        }
    }
}
