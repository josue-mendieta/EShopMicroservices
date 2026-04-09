using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// Clase que implementa un comportamiento de pipeline para registrar información sobre la solicitud y respuesta de los manejadores de MediatR.
/// </summary>
/// <typeparam name="TRequest">Tipo de la solicitud que se está manejando. Debe implementar IRequest<TResponse>.</typeparam>
/// <typeparam name="TResponse">Tipo de la respuesta que se espera del manejador. Debe ser un tipo no nulo.</typeparam>
/// <param name="logger">Dependencia de ILogger para registrar información sobre la solicitud y respuesta. Se inyecta a través del constructor.</param>
public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
    )
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
{

    /// <summary>
    /// Método que se ejecuta antes y después de la ejecución del manejador de MediatR. Registra información sobre la solicitud, la respuesta y el tiempo que tomó procesar la solicitud.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope("Handling {Request} with response {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

        logger.LogInformation("[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = Stopwatch.StartNew();
        timer.Start();

        var response = await next(cancellationToken);

        timer.Stop();

        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMACE] The Request={Request} took TimeTaken={TimeTaken} seconds", typeof(TRequest).Name, timeTaken.TotalSeconds);
        }

        logger.LogInformation("[END] Handle Request={Request} with Response={Response}", typeof(TRequest).Name, response);

        return response;
    }
}
