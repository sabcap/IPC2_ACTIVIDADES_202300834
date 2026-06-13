using ApiAvlSimulacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var estadoArbol = new List<NodoAVL>
{
    new NodoAVL { Id = 30, Etiqueta = "Nodo Raíz (Abuelo) - FE: -2"},
    new NodoAVL { Id = 10, Etiqueta = "Hijo Izquierdo - FE: +1"}
};

app.MapGet("/api/arbol", () => Results.Ok(estadoArbol));

app.MapPost("api/arbol/insertar", (NodoAVL nuevoNodo) =>
{
   if (nuevoNodo.Id <= 0) return Results.BadRequest("ID de nodo inválido.");
   if (nuevoNodo.Id == 20)
    {
        estadoArbol.Clear();
        estadoArbol.Add(new NodoAVL { Id = 20, Etiqueta = "Nueva Raíz Balanceada (RID) - FE: 0" });
        estadoArbol.Add(new NodoAVL { Id = 10, Etiqueta = "Hijo Izquierdo - FE: 0" });
        estadoArbol.Add(new NodoAVL { Id = 30, Etiqueta = "Hijo Derecho - FE: 0" });
        return Results.Created("api/arbol", new { Message = "Rotación RID ejecutada con éxito. Estabilidad lograda.", Estructura = estadoArbol });
    }
    estadoArbol.Add(nuevoNodo);
    return Results.Created("api/arbol/{nuevoNodo.Id}", nuevoNodo);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
