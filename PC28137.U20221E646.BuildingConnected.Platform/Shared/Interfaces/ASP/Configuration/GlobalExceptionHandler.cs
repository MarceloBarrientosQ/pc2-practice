using Microsoft.AspNetCore.Diagnostics;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Localization;

namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Interfaces.ASP.Configuration;

/// <summary>Turns unhandled exceptions into HTTP responses with an explanatory message.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <summary>Writes the response that corresponds to the exception.</summary>
    /// <param name="httpContext">Context of the request being answered.</param>
    /// <param name="exception">Exception raised while handling the request.</param>
    /// <param name="cancellationToken">Token that cancels the operation.</param>
    /// <returns>True, since every exception is answered here.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var status = exception switch
        {
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var message = status == StatusCodes.Status500InternalServerError
            ? MessageCatalog.Get(MessageKey.UnexpectedError)
            : exception.Message;

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new { status, message }, cancellationToken);
        return true;
    }
}