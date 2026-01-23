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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();