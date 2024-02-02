using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Application.Heroes.Persistence;
using TourOfHeroes.Contracts.Heroes;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Api.Controllers
{
    public class HeroesController(IHeroRepository heroRepository) : ApiController
    {
        private readonly IHeroRepository _heroRepository = heroRepository;

        // GET: api/<HeroesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<HeroResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            ErrorOr<List<Hero>> getHeroesResult = await _heroRepository.GetHeroes(cancellationToken);

            return getHeroesResult.Match(heroes => {
                List<HeroResponse> response = [];
                heroes.ForEach(hero => response.Add(MapHeroResponse(hero)));
                return Ok(response);
            }, Problem);
        }

        // GET api/<HeroesController>/<Guid>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(HeroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            ErrorOr<Hero> getHeroResult = await _heroRepository.GetHero(id, cancellationToken);
            
            return getHeroResult.Match(hero => Ok(MapHeroResponse(hero)), Problem);
        }

        // POST api/<HeroesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateHeroRequest request, CancellationToken cancellationToken)
        {
            ErrorOr<Hero> requestToHeroResult = Hero.From(request);

            if (requestToHeroResult.IsError)
            {
                return Problem(requestToHeroResult.Errors);
            }

            var hero = requestToHeroResult.Value;
            ErrorOr<Created> createHeroResult = await _heroRepository.CreateHero(hero, cancellationToken);

            return createHeroResult.Match(
                created => CreatedAtGetHero(hero),
                Problem);
        }

        // PUT api/<HeroesController>/<Guid>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateHeroRequest request, CancellationToken cancellationToken)
        {
            ErrorOr<Hero> requestToHeroResult = Hero.From(id, request);

            if (requestToHeroResult.IsError)
            {
                return Problem(requestToHeroResult.Errors);
            }

            var hero = requestToHeroResult.Value;
            if (await _heroRepository.Exists(hero.Id, cancellationToken))
            {
                ErrorOr<Updated> updateHeroResult = await _heroRepository.UpdateHero(hero, cancellationToken);
                return updateHeroResult.Match(updated => NoContent(), Problem);
            }
            else
            {
                ErrorOr<Created> createHeroResult = await _heroRepository.CreateHero(hero, cancellationToken);
                return createHeroResult.Match(created => CreatedAtGetHero(hero), Problem);
            }
        }

        // DELETE api/<HeroesController>/<Guid>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            ErrorOr<Deleted> deleteHeroResult = await _heroRepository.DeleteHero(id, cancellationToken);

            return deleteHeroResult.Match(deleted => NoContent(), Problem);
        }

        private static HeroResponse MapHeroResponse(Hero hero) => new(hero.Id, hero.Name);

        private CreatedAtActionResult CreatedAtGetHero(Hero hero)
        {
            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { id = hero.Id },
                value: MapHeroResponse(hero));
        }
    }
}
