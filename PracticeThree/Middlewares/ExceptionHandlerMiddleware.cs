using System.Globalization;

namespace UPB.PracticeThree.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception ex)
        {
            // LOG ex.Message
            HandleException(context, ex);
        }
        
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "text/json";
        //context.Response.StatusCode = 500;
        return context.Response.WriteAsync("Sucedio el siguiente error: " + ex.Message);
    }
}

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}