using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

//Se registran los servicios necesarios para utilizar Carter en la aplicación, asi como los Endpoints definidos con Carter, es decir, los módulos que implementan ICarterModule.
builder.Services.AddCarter();


builder.Services.AddMediatR(config =>
{
    //Registrar todos los manejadores (handlers) desde el ensamblado actual, considerando que los manejadores implementan IRequest de MediatR.
    // En este caso, los manejadores pueden ser tanto comandos (ICommandHandler) como consultas (IQueryHandler).
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMarten(options =>
{
    // Configurar la cadena de conexión a la base de datos PostgreSQL.
    options.Connection(builder.Configuration.GetConnectionString("Database")!);

    //El valor por defecto es CreateOrUpdate, que crea los objetos del esquema si no existen o los actualiza si ya existen, es para desarrollo.
    //options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

}).UseLightweightSessions();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();