using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TourOfHeroes.Domain.Common.Models;
using TourOfHeroes.Domain.Heroes;
using TourOfHeroes.Domain.Users;
using TourOfHeroes.Infrastructure.Interceptors;

namespace TourOfHeroes.Infrastructure.Common.Persistence
{
    public sealed class TourOfHeroesDbContext(DbContextOptions<TourOfHeroesDbContext> _options, PublicDomainEventsInterceptor _publishDomainEventsInterceptor) : DbContext(_options)
    {
        public DbSet<Hero> Heroes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.IsPrimaryKey())
                .ToList()
                .ForEach(p => p.ValueGenerated = ValueGenerated.Never);

            modelBuilder.Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(TourOfHeroesDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
