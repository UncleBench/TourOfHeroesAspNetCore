using Mapster;
using TourOfHeroes.Application.Authentication.Commands;
using TourOfHeroes.Application.Authentication.Common;
using TourOfHeroes.Application.Authentication.Queries;
using TourOfHeroes.Contracts.Authentication;

namespace TourOfHeroes.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
        }
    }
}
