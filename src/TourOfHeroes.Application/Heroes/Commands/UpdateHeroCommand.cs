using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public record UpdateHeroCommand(string Name) : IRequest<ErrorOr<Updated>>;

    public class UpdateHeroCommandHandler(IHeroRepository heroRepository) : IRequestHandler<UpdateHeroCommand, ErrorOr<Updated>>
    {
        public Task<ErrorOr<Updated>> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var hero = new Hero(request.Name);

            return heroRepository.UpdateHero(hero, cancellationToken);
        }
    }
}
