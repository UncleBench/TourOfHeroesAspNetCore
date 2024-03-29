﻿using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Application.Heroes.Commands;
using TourOfHeroes.Application.Heroes.Queries;
using TourOfHeroes.Contracts.Heroes;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Api.Controllers
{
    public sealed class HeroesController(IMediator _mediator, IMapper _mapper) : ApiController
    {
        // GET: api/<HeroesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<HeroResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var getHeroesQuery = new GetHeroesQuery();
            var getHeroesQueryResult = await _mediator.Send(getHeroesQuery, cancellationToken);

            return getHeroesQueryResult.Match(heroes =>
            {
                List<HeroResponse> response = [];
                heroes.ForEach(hero => response.Add(_mapper.Map<HeroResponse>(hero)));
                return Ok(response);
            }, Problem);
        }

        // GET api/<HeroesController>/<Guid>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(HeroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var getHeroQuery = new GetHeroQuery(id);
            var getHeroQueryResult = await _mediator.Send(getHeroQuery, cancellationToken);

            return getHeroQueryResult.Match(hero => Ok(_mapper.Map<HeroResponse>(hero)), Problem);
        }

        // POST api/<HeroesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateHeroRequest request, CancellationToken cancellationToken)
        {
            var createHeroCommand = _mapper.Map<CreateHeroCommand>(request);
            var createHeroCommandResult = await _mediator.Send(createHeroCommand, cancellationToken);

            return createHeroCommandResult.Match(
                hero => CreatedAtAction(
                    actionName: nameof(Get),
                    routeValues: new { hero.Id },
                    value: _mapper.Map<HeroResponse>(hero)),
                Problem);
        }

        // PUT api/<HeroesController>/<Guid>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateHeroRequest request, CancellationToken cancellationToken)
        {
            var getHeroQuery = new GetHeroQuery(id);
            var getHeroQueryResult = await _mediator.Send(getHeroQuery, cancellationToken);

            if (getHeroQueryResult.FirstError == HeroErrors.NotFound)
            {
                var createHeroCommand = _mapper.Map<CreateHeroCommand>(request);
                var createHeroCommandResult = await _mediator.Send(createHeroCommand, cancellationToken);

                return createHeroCommandResult.Match(
                    hero => CreatedAtAction(
                        actionName: nameof(Get),
                        routeValues: new { hero.Id },
                        value: _mapper.Map<HeroResponse>(hero)),
                    Problem);
            }
            else
            {
                var updateHeroCommand = _mapper.Map<UpdateHeroCommand>((request, id));
                var updateHeroCommandResult = await _mediator.Send(updateHeroCommand, cancellationToken);

                return updateHeroCommandResult.Match(updatedHero => NoContent(), Problem);
            }
        }

        // DELETE api/<HeroesController>/<Guid>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var deleteHeroCommand = new DeleteHeroCommand(id);
            var deleteHeroCommandResult = await _mediator.Send(deleteHeroCommand, cancellationToken);

            return deleteHeroCommandResult.Match(deleted => NoContent(), Problem);
        }
    }
}
