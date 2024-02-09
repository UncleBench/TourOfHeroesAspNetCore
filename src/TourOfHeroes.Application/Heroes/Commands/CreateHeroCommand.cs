using ErrorOr;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;
using TourOfHeroes.Domain.Heroes.Entities;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public sealed record CreateHeroCommand(
        string Name, 
        List<SuperPowerCommand>? SuperPowers) 
        : IRequest<ErrorOr<Hero>>;

    public sealed record SuperPowerCommand(
        string Name, 
        string Description) 
        : IRequest<ErrorOr<SuperPower>>;


    public sealed class CreateHeroCommandHandler(IHeroRepository _heroRepository) : IRequestHandler<CreateHeroCommand, ErrorOr<Hero>>
    {
        public async Task<ErrorOr<Hero>> Handle(CreateHeroCommand command, CancellationToken cancellationToken)
        {
            var superPowers = command.SuperPowers?
                .ConvertAll(x => SuperPower.Create(
                    x.Name,
                    x.Description));

            var newHero = Hero.Create(
                command.Name,
                superPowers);

            return await _heroRepository.CreateHero(newHero, cancellationToken);
        }
    }

    public sealed class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
    {
        public CreateHeroCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public sealed class SuperPowerCommandValidator : AbstractValidator<SuperPowerCommand>
    {
        public SuperPowerCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
