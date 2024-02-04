using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Application.Heroes.Queries
{
    public sealed record GetHeroQuery(Guid Id) : IRequest<ErrorOr<Hero>>;

    public sealed class GetHeroQueryHandler(IHeroRepository _heroRepository) : IRequestHandler<GetHeroQuery, ErrorOr<Hero>>
    {
        public async Task<ErrorOr<Hero>> Handle(GetHeroQuery query, CancellationToken cancellationToken)
        {
            return await _heroRepository.GetHero(query.Id, cancellationToken);
        }
    }

    public sealed class GetHeroQueryValidator : AbstractValidator<GetHeroQuery>
    {
        public GetHeroQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
