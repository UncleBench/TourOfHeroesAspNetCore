using ErrorOr;
using TourOfHeroes.Server.Models;

namespace TourOfHeroes.Server.Services.Heroes
{
    public interface IHeroService
    {
        Task<bool> Exists(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Created>> CreateHero(Hero hero, CancellationToken cancellationToken);
        Task<ErrorOr<Deleted>> DeleteHero(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<List<Hero>>> GetHeroes(CancellationToken cancellationToken);
        Task<ErrorOr<Hero>> GetHero(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<Updated>> UpdateHero(Hero hero, CancellationToken cancellationToken);
    }
}