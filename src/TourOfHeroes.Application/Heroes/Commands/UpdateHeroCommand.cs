using ErrorOr;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public sealed record UpdateHeroCommand(Guid Id, string Name) : IRequest<ErrorOr<Updated>>;

    public sealed class UpdateHeroCommandHandler(IHeroRepository _heroRepository) : IRequestHandler<UpdateHeroCommand, ErrorOr<Updated>>
    {
        public async Task<ErrorOr<Updated>> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var hero = await _heroRepository.GetHero(request.Id, cancellationToken);

            if (hero.IsError)
            {
                return hero.Errors;
            }
            else
            {
                var updatedHero = hero.Value;
                updatedHero.Name = request.Name;

                return await _heroRepository.UpdateHero(updatedHero, cancellationToken);
            }
        }
    }
}
