using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Quries.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDiner.WebApi.Common.Mapping;


public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest=>dest.Token,src=>src.Token)
        .Map(dest=>dest,src=>src.user);
    }
}
