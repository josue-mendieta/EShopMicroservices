using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Defines a request that returns a response of the specified type when executed as a query.
/// Define una solicitud que devuelve una respuesta del tipo especificado cuando se ejecuta como una consulta.
/// </summary>
/// <typeparam name="TResponse">
/// The type of the response returned by the query. This type must not be null.
/// El tipo de la respuesta devuelta por la consulta. Este tipo no debe ser nulo.
/// </typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}