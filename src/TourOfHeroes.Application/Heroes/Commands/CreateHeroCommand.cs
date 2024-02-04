using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public sealed record CreateHeroCommand(string Name) : IRequest<ErrorOr<Hero>>;

    public sealed class CreateHeroCommandHandler(IHeroRepository _heroRepository) : IRequestHandler<CreateHeroCommand, ErrorOr<Hero>>
    {
        public async Task<ErrorOr<Hero>> Handle(CreateHeroCommand command, CancellationToken cancellationToken)
        {
            var newHero = Hero.Create(command.Name);
            return await _heroRepository.CreateHero(newHero, cancellationToken);
        }
    }
}
