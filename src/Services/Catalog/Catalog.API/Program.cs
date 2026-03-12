var builder = WebApplication.CreateBuilder(args);


var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    //Registrar todos los manejadores (handlers) desde el ensamblado actual, considerando que los manejadores implementan IRequest de MediatR.
    // En este caso, los manejadores pueden ser tanto comandos (ICommandHandler) como consultas (IQueryHandler).
    config.RegisterServicesFromAssembly(assembly);

    //Registrar el comportamiento de validación (ValidationBehavior) para que se ejecute antes de los manejadores de comandos o consultas, permitiendo que las validaciones definidas con FluentValidation se apliquen automáticamente a las solicitudes entrantes.
    // ValidationBehavior<,> significa que se aplicará a cualquier comando o consulta, independientemente de su tipo de resultado, siempre y cuando implementen IRequest de MediatR.
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

//Se registran los servicios necesarios para utilizar Carter en la aplicación, asi como los Endpoints definidos con Carter, es decir, los módulos que implementan ICarterModule.
builder.Services.AddCarter();


builder.Services.AddMarten(options =>
{
    // Configurar la cadena de conexión a la base de datos PostgreSQL.
    options.Connection(builder.Configuration.GetConnectionString("Database")!);

    //El valor por defecto es CreateOrUpdate, que crea los objetos del esquema si no existen o los actualiza si ya existen, es para desarrollo.
    //options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

}).UseLightweightSessions();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.UseExceptionHandler(options =>
{
    
});

app.Run();