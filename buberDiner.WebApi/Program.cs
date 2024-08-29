
using BuberDiner.WebApi.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using BuberDinner.WebApi.Filter;
using BuberDinner.WebApi.MiddleWare;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
{
builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<ProblemDetailsFactory,BuberDinerProblemDetailsFactory>();
//to avoid adding filter attribute on every controller
//builder.Services.AddControllers(options=>options.Filters.Add<ErrorHandlingFilterAttribute>());
builder.Services.AddControllers();
}

var app = builder.Build();
app.UseExceptionHandler("/errors");
//app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
