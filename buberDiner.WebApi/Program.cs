

using BuberDinner.Application;
using BuberDinner.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

{
builder.Services.AddPresentation().AddApplication().AddInfrastructure(builder.Configuration);


}

var app = builder.Build();
app.UseExceptionHandler("/errors");
//app.UseMiddleware<ErrorHandlingMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
