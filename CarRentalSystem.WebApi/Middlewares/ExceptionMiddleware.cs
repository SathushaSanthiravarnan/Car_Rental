using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CarRentalSystem.WebApi.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (ValidationException ex) // FluentValidation
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ctx.Response.ContentType = "application/json";

                var payload = new
                {
                    status = ctx.Response.StatusCode,
                    title = "Validation failed",
                    errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage })
                };

                var json = JsonSerializer.Serialize(payload);
                await ctx.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                // fallback 500
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ctx.Response.ContentType = "application/json";

                var payload = new
                {
                    status = ctx.Response.StatusCode,
                    title = "Unexpected error",
                    detail = ex.Message            // prod-ல் stack trace expose பண்ணாதீங்க
                };

                await ctx.Response.WriteAsJsonAsync(payload);
            }
        }
    }
}
