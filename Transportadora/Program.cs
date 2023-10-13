
try
{
 var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();   // C�digo que puede generar una excepci�n
}
catch (Exception ex)
{
    // Registra el mensaje de error y la pila de llamadas en un registro o en la salida de la aplicaci�n
    Console.WriteLine("Se ha producido un error: " + ex.Message);
    Console.WriteLine("Stack Trace: " + ex.StackTrace);
}
