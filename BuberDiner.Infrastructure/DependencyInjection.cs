
using BuberDiner.Infrastructure.Persistence;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using BuberDinner.Application.common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection{
public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurations){    
    services.Configure<JwtTokenSettings>(configurations.GetSection(JwtTokenSettings.SectionName));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IUserRepository,UserRepository>();
     return services;
}
}
