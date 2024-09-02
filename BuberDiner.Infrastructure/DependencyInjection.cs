
using System.Text;
using BuberDiner.Infrastructure.Persistence;
using BuberDinner.Application.common.Interfaces.Authentication;
using BuberDinner.Application.common.Interfaces.Persistence;
using BuberDinner.Application.common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection{
public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configurations){    
    services.AddAuth(configurations);
    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<IUserRepository,UserRepository>();
     return services;
}

public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configurations){    
    
    var JwtSettings = new JwtTokenSettings();
    configurations.Bind(JwtTokenSettings.SectionName,JwtSettings);
    
    //services.Configure<JwtTokenSettings>(configurations.GetSection(JwtTokenSettings.SectionName));
    //replaced by 
    services.AddSingleton(Options.Create(JwtSettings));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

    services.AddAuthentication(defaultScheme:JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=> options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtSettings.Issuer,
        ValidAudience = JwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(JwtSettings.Secret)
        )
    });
    return services;
}

}