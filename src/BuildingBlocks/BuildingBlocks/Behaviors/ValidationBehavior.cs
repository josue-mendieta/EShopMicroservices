using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

/// <summary>
/// Provides a pipeline behavior that performs validation on incoming requests using the specified validators before passing the request to the next handler.
/// Proporciona un comportamiento de pipeline que realiza la validación de las solicitudes entrantes utilizando los validadores especificados antes de pasar la solicitud al siguiente manejador.
/// </summary>
/// <remarks>
/// 
/// If any validation failures are detected, a <see cref="ValidationException"/> is thrown and the request is not processed further. 
/// Si se detectan fallas de validación, se lanza una <see cref="ValidationException"/> y la solicitud no se procesa más.
/// 
/// This behavior should be registered in the pipeline to enforce validation consistently for all requests.
/// Este comportamiento debe registrarse en el pipeline para hacer cumplir la validación de manera consistente para todas las solicitudes.
/// 
/// Thread safety depends on the underlying validators.
/// La seguridad de subprocesos depende de los validadores subyacentes. 
/// 
/// Ensure that any shared resources used by the validators are thread-safe to avoid concurrency issues.
/// Asegúrese de que cualquier recurso compartido utilizado por los validadores sea seguro para subprocesos para evitar problemas de concurrencia.
/// </remarks>
/// <typeparam name="TRequest">
/// The type of the request to be validated. Must implement <see cref="ICommand{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the request handler.</typeparam>
/// <param name="validators">A collection of validators that are applied to the incoming request. Each validator is invoked to ensure the request
/// meets defined validation rules.</param>
public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
    )
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    /// <summary>
    /// Validates the specified request using all configured validators and invokes the next handler in the pipeline if validation succeeds.    
    /// Valida la solicitud especificada utilizando todos los validadores configurados e invoca el siguiente manejador en el pipeline si la validación tiene éxito.
    /// </summary>
    /// <remarks>
    /// If validation fails, the method throws a ValidationException containing all validation errors
    /// and does not invoke the next handler. All validators are executed asynchronously before proceeding.
    /// Si la validación falla, el método lanza una ValidationException que contiene todos los errores de validación y no invoca el siguiente manejador. Todos los validadores se ejecutan de forma asincrónica antes de continuar.
    /// </remarks>
    /// <param name="request">The request object to be validated and processed. Cannot be null.</param>
    /// <param name="next">A delegate representing the next handler to invoke in the pipeline. Called if validation passes.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response returned by the next
    /// handler.</returns>
    /// <exception cref="ValidationException">Thrown if one or more validation failures are detected in the request.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}
