using ErrorOr;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Application.Heroes.Queries
{
    public record GetHeroesQuery : IRequest<ErrorOr<List<Hero>>>;

    public class GetHeroesQueryHandler(IHeroRepository _heroRepository) : IRequestHandler<GetHeroesQuery, ErrorOr<List<Hero>>>
    {
        public async Task<ErrorOr<List<Hero>>> Handle(GetHeroesQuery request, CancellationToken cancellationToken)
        {
            return await _heroRepository.GetHeroes(cancellationToken);
        }
    }
}
