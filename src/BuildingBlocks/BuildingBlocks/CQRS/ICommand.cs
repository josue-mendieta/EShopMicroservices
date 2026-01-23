using MediatR;

namespace BuildingBlocks.CQRS;

/// <summary>
/// Defines a command that can be sent to a request handler and does not return a result value.
/// Define un comando que puede ser enviado a un manejador de solicitudes y no devuelve un valor de resultado.
/// </summary>
/// <remarks>
/// Implement this interface to represent an operation or action that modifies state or triggers side effects, but does not produce a return value. 
/// Typically used in the Command pattern or with mediator libraries to encapsulate requests that do not require a response.
/// 
/// Implemente esta interfaz para representar una operación o acción que modifica el estado o desencadena efectos secundarios, pero no produce un valor de retorno.
/// Típicamente se utiliza en el patrón de Comando o con bibliotecas mediadoras para encapsular solicitudes que no requieren una respuesta.
/// 
/// </remarks>
public interface ICommand : IRequest<Unit>
{
}

/// <summary>
/// Defines a request that can be executed to produce a response of the specified type.
/// Define una solicitud que puede ser ejecutada para producir una respuesta del tipo especificado.
/// </summary>
/// <remarks>
/// Implement this interface to represent a command in a request/response pattern. 
/// Typically used in mediator-based architectures to encapsulate an action or operation that returns a result.
/// 
/// Implemente esta interfaz para representar un comando en un patrón de solicitud/respuesta.
/// Típicamente se utiliza en arquitecturas basadas en mediadores para encapsular una acción u operación que devuelve un resultado.
/// 
/// </remarks>
/// <typeparam name="TResponse">
/// The type of the response returned when the command is executed.
/// El tipo de la respuesta devuelta cuando se ejecuta el comando.
/// </typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}