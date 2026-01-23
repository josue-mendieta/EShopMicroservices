using MediatR;

namespace BuildingBlocks.CQRS;


/// <summary>
/// Defines a handler for processing a command that does not return a result value.
/// Define un manejador para procesar un comando que no devuelve un valor de resultado.
/// </summary>
/// <remarks>
/// 
/// This interface is typically used for commands that perform an action without returning a value.
/// Implementations should encapsulate the logic required to handle the specified command type. 
/// For commands that return a result, use <see cref="ICommandHandler{TCommand, TResult}"/> instead.
/// 
/// Esta interfaz se utiliza típicamente para comandos que realizan una acción sin devolver un valor.
/// Las implementaciones deben encapsular la lógica requerida para manejar el tipo de comando especificado.
/// Para comandos que devuelven un resultado, use <see cref="ICommandHandler{TCommand, TResult}"/> en su lugar.
/// 
/// </remarks>
/// <typeparam name="TCommand">
/// The type of command to handle. Must implement <see cref="ICommand{Unit}"/>.
/// El tipo de comando a manejar. Debe implementar <see cref="ICommand{Unit}"/>.
/// </typeparam>
public interface ICommandHandler<in TCommand> :
    ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{
}


/// <summary>
/// <para>Defines a handler for processing a command and returning a response.</para>
/// <para>Define un manejador para procesar un comando y devolver una respuesta.</para>
/// </summary>
/// 
/// <remarks>
/// <para>
/// Implement this interface to handle command-based requests in a request/response pattern. Typically
/// used in applications following the CQRS (Command Query Responsibility Segregation) pattern to encapsulate command handling logic.
/// </para>
/// <para>
/// Implementa esta interfaz para manejar solicitudes basadas en comandos en un patrón de solicitud/respuesta. Típicamente
/// usado en aplicaciones que siguen el patrón CQRS (Command Query Responsibility Segregation) para encapsular la lógica de manejo de comandos.
/// </para>
/// 
/// </remarks>
/// 
/// <typeparam name="TCommand">
/// <para>The type of command to handle. Must implement <see cref="ICommand{TResponse}"/>.</para>
/// <para>El tipo de comando a manejar. Debe implementar <see cref="ICommand{TResponse}"/>.</para>
/// </typeparam>
/// <typeparam name="TResponse">
/// <para>The type of response returned by the handler. Must not be null.</para>
/// <para>El tipo de respuesta devuelta por el manejador. No debe ser nulo.</para>
/// </typeparam>
public interface ICommandHandler<in TCommand, TResponse> : 
    IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}
