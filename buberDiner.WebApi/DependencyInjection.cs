using BuberDiner.WebApi.Common.Mapping;
using BuberDiner.WebApi.Errors;
using BuberDinner.WebApi;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependencyInjection{
public static IServiceCollection AddPresentation(this IServiceCollection services){    
     services.AddMappings();
     services.AddSingleton<ProblemDetailsFactory,BuberDinerProblemDetailsFactory>();
//to avoid adding filter attribute on every controller
//builder.Services.AddControllers(options=>options.Filters.Add<ErrorHandlingFilterAttribute>());
      services.AddControllers();
     return services;
}
}
