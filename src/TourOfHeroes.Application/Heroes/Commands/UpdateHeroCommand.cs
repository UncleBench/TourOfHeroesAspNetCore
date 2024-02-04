using ErrorOr;
using FluentValidation;
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
        public async Task<ErrorOr<Updated>> Handle(UpdateHeroCommand command, CancellationToken cancellationToken)
        {
            var hero = await _heroRepository.GetHero(command.Id, cancellationToken);

            if (hero.IsError)
            {
                return hero.Errors;
            }
            else
            {
                var updatedHero = hero.Value;
                updatedHero.Name = command.Name;

                return await _heroRepository.UpdateHero(updatedHero, cancellationToken);
            }
        }
    }

    public sealed class UpdateHeroCommandValidator : AbstractValidator<UpdateHeroCommand>
    {
        public UpdateHeroCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
