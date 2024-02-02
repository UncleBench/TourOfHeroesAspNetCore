using ErrorOr;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Application.Heroes.Persistence
{
    public interface IHeroRepository
    {
        Task<bool> Exists(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Created>> CreateHero(Hero hero, CancellationToken cancellationToken);
        Task<ErrorOr<Deleted>> DeleteHero(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<List<Hero>>> GetHeroes(CancellationToken cancellationToken);
        Task<ErrorOr<Hero>> GetHero(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Updated>> UpdateHero(Hero hero, CancellationToken cancellationToken);
    }
}