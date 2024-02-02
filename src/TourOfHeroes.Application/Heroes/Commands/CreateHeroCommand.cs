using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public record CreateHeroCommand(string Name) : IRequest<ErrorOr<Hero>>;

    public class CreateHeroCommandHandler(IHeroRepository _heroRepository) : IRequestHandler<CreateHeroCommand, ErrorOr<Hero>>
    {
        public async Task<ErrorOr<Hero>> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
        {
            var newHero = new Hero(request.Name);
            return await _heroRepository.CreateHero(newHero, cancellationToken);
        }
    }
}
