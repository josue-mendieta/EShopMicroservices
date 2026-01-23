using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Defines a handler that processes a query and returns a response of the specified type.
/// Define un manejador que procesa una consulta y devuelve una respuesta del tipo especificado.
/// </summary>
/// 
/// <remarks>
/// Implementations of this interface are responsible for handling queries and producing corresponding responses. 
/// This interface is typically used in request-response or CQRS patterns to decouple query logic from its execution.
/// 
/// Las implementaciones de esta interfaz son responsables de manejar consultas y producir respuestas correspondientes.
/// Esta interfaz se utiliza típicamente en patrones de solicitud-respuesta o CQRS para desacoplar la lógica de consulta de su ejecución.
/// 
/// </remarks>
/// 
/// <typeparam name="TQuery">
/// The type of query to be handled. Must implement <see cref="IQuery{TResponse}"/>.
/// El tipo de consulta a manejar. Debe implementar <see cref="IQuery{TResponse}"/>.
/// </typeparam>
/// <typeparam name="TResponse">
/// The type of response returned by the handler. Must not be null.
/// El tipo de respuesta devuelta por el manejador. No debe ser nulo.
/// </typeparam>
public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
