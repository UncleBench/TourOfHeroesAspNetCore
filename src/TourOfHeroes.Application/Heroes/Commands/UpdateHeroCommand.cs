using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes.Entities;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public sealed record UpdateHeroCommand(
        Guid Id, 
        string Name, 
        List<SuperPowerCommand>? SuperPowers) 
        : IRequest<ErrorOr<Updated>>;

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
                var superPowers = command.SuperPowers?
                    .ConvertAll(x => SuperPower.Create(
                        x.Name,
                        x.Description)) ?? [];

                var updatedHero = hero.Value;
                updatedHero.Name = command.Name;
                updatedHero.SuperPowers = superPowers;

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
