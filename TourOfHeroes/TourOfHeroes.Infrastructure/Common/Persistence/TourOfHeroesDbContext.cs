using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Infrastructure.Common.Persistence
{
    public class TourOfHeroesDbContext : DbContext
    {
        public TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> options) : base(options) { }

        public DbSet<Hero> Heroes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TourOfHeroesDbContext).Assembly);
        }
    }
}
