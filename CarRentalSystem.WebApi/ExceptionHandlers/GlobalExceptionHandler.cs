using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.ExceptionHandlers
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext ctx,
            Exception ex,
            CancellationToken ct)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected error",
                Detail = ex.Message 
            };

            ctx.Response.StatusCode = problem.Status!.Value;
            await ctx.Response.WriteAsJsonAsync(problem, ct);

            return true;
        }
    }
}
