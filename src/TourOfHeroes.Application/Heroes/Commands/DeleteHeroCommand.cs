using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;

namespace TourOfHeroes.Application.Heroes.Commands
{
    public sealed record DeleteHeroCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;

    public sealed class DeleteHeroCommandHandler(IHeroRepository _heroRepository) : IRequestHandler<DeleteHeroCommand, ErrorOr<Deleted>>
    {
        public async Task<ErrorOr<Deleted>> Handle(DeleteHeroCommand command, CancellationToken cancellationToken)
        {
            return await _heroRepository.DeleteHero(command.Id, cancellationToken);
        }
    }

    public sealed class DeleteHeroCommandValidator : AbstractValidator<DeleteHeroCommand>
    {
        public DeleteHeroCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
