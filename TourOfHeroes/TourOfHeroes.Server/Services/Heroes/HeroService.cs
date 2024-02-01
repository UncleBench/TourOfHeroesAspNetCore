﻿using ErrorOr;
using TourOfHeroes.Server.Persistence;
using TourOfHeroes.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace TourOfHeroes.Server.Services.Heroes
{
    public class HeroService : IHeroService
    {
        private readonly TourOfHeroesDbContext _dbContext;

        public HeroService(TourOfHeroesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Exists(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Heroes.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<ErrorOr<Created>> CreateHero(Hero hero, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(hero, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Created;
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
    }
}