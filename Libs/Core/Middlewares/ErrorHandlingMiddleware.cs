using System.Net;
using Core.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseCustomException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            context.Response.ContentType = "application/json";
            
            var errorResponse = new { message = ex.Message };
            
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(jsonErrorResponse);
        }
    }
}