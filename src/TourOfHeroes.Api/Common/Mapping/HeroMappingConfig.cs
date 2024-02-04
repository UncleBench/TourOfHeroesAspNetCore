using Mapster;
using TourOfHeroes.Application.Heroes.Commands;
using TourOfHeroes.Contracts.Heroes;
using TourOfHeroes.Domain.Heroes;

namespace TourOfHeroes.Api.Common.Mapping
{
    public sealed class HeroMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateHeroRequest, CreateHeroCommand>();
            config.NewConfig<(UpdateHeroRequest Request, Guid Id), UpdateHeroCommand>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest, src => src.Request);
            
            config.NewConfig<HeroResponse, Hero>();
        }
    }
}
