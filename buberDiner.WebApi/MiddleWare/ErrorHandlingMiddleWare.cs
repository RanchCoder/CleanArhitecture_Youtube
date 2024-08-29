using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuberDinner.WebApi.MiddleWare;

public class ErrorHandlingMiddleWare{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleWare(RequestDelegate next){
        _next = next; 
    }

    public async Task Invoke(HttpContext httpContext){
        try{
            await _next(httpContext);
        }
        catch(Exception e){
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception e)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new {error = e.Message});
        httpContext.Response.ContentType =  "application/json";
        httpContext.Response.StatusCode = (int)statusCode;
        return httpContext.Response.WriteAsync(result);
    }
}