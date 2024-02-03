using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;
using TourOfHeroes.Infrastructure.Common.Persistence;

namespace TourOfHeroes.Infrastructure.Heroes.Persistence
{
    public sealed class HeroRepository(TourOfHeroesDbContext _dbContext) : IHeroRepository
    {
        public async Task<ErrorOr<Hero>> CreateHero(Hero hero, CancellationToken cancellationToken)
        {
            var addResult = await _dbContext.AddAsync(hero, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return addResult.Entity;
        }

        public async Task<ErrorOr<List<Hero>>> GetHeroes(CancellationToken cancellationToken)
        {
            return await _dbContext.Heroes.ToListAsync(cancellationToken);
        }

        public async Task<ErrorOr<Hero>> GetHero(Guid id, CancellationToken cancellationToken)
        {
            var hero = await _dbContext.Heroes.FindAsync(id, cancellationToken);

            return hero is not null ? hero : HeroErrors.NotFound;
        }

        public async Task<ErrorOr<Updated>> UpdateHero(Hero hero, CancellationToken cancellationToken)
        {
            _dbContext.Update(hero);
            await _dbContext.SaveChangesAsync(true, cancellationToken);

            return Result.Updated;
        }

        public async Task<ErrorOr<Deleted>> DeleteHero(Guid id, CancellationToken cancellationToken)
        {
            bool heroExists = await Exists(id, cancellationToken);

            if (!heroExists)
            {
                return HeroErrors.NotFound;
            }

            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }

        private async Task<bool> Exists(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Heroes.AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}
