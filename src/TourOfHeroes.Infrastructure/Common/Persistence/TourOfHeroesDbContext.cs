using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Domain.Heroes;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Infrastructure.Common.Persistence
{
    public sealed class TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> options) : DbContext(options)
    {
        public DbSet<Hero> Heroes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TourOfHeroesDbContext).Assembly);
        }
    }
}
