using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Defines a handler for processing queries that return a response.
/// Define un manejador para procesar consultas que devuelven una respuesta.
/// </summary>
/// <remarks>
/// Implement this interface to handle query requests in a request-response pattern.
/// Implementa esta interfaz para manejar solicitudes de consulta en un patrón de solicitud-respuesta.
/// Typically used in applications following the CQRS (Command Query Responsibility Segregation) pattern to separate query logic from command handling.
/// Típicamente se utiliza en aplicaciones que siguen el patrón CQRS (Command Query Responsibility Segregation) para separar la lógica de consulta del manejo de comandos.
/// 
/// </remarks>
/// <typeparam name="TQuery">The type of the query to handle. Must implement <see cref="IQuery{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the handler. Must not be null.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
