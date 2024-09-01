using System.Reflection;
using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common.Behavior;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependencyInjection{
public static IServiceCollection AddApplication(this IServiceCollection services){    
     services.AddMediatR(typeof(DependencyInjection).Assembly);
     // services.AddScoped<IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, ValidationBehavior>();
     services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
     return services;
}
}
