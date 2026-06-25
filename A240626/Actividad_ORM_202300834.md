# De ADO.NET Tradicional a la Automatización con EF CORE

- **Nombre**: Josué Javier Carrera Soyós
- **Carné**: 202300834
- **Asignatura**: Introducción a la Programación y Computación 2

## Diagnóstico Técnico y Brecha de Impedencia

### 1. Brecha de Impedancia:

- **Dominio de Objetos (C#)**: Se rige por principios de la programación orientada a objetos (POO), estructurando los datos mediante encapsulamiento, herencia, polimorfismo y relaciones lógicas basadas en referencias en memoria.

- **Dominio Relacional (SQL)**: Se fundamenta en el modelo relacional matemático, organizando los datos en tablas bidimensionales (filas y columnas) vinculadas estrictamente por llaves primarias y foráneas, sin conceptos nativos de comportamiento o herencia.

**Equivalencias del mapeador (ORM)**

- **Clase Clásica (POO) -> Mapea a -> Tabla**
- **Propiedad/Atributo -> Mapea a -> Columna**
- **Instancia de Objeto -> Mapea a -> Fila**

### 2. Mitigación de Vulnerabilidades:
- **En EF Core**: La propiedad nativa que previene de forma automatizada la Inyección SQL son las Consultas Parametrizadas (Parameterized Queries). EF Core traduce automáticamente las expresiones LINQ utilizando parámetros en lugar de concatenar texto plano.

- **Comando equivalente en ADO.NET tradicional**: Se utilizaba la clase `SqlCommand` junto con su colección de parámetros mediante `SqlParameterCollection` (específicamente con métodos como `.Parameters.AddWithValue()` o `.Parameters.Add()`).

### 3. Optimización de Infraestructura:

El uso de `.AsNoTracking()` es una muestra de solidaridad computacional porque desactiva el mecanismo de seguimiento de cambios (Change Tracker) en el contexto de EF Core. Al hacer esto:

- Se reduce el consumo de memoria y CPU, ya que EF Core no necesita mantener un registro de las entidades recuperadas para detectar cambios.

- Se mejora el rendimiento en escenarios de solo lectura, donde no se requiere actualizar los datos, permitiendo que las consultas sean más rápidas y eficientes.

## Desafío de Refactorización de Código

### Versión moderna con EF Core:

- **El Contexto**:
```
using Microsoft.EntityFrameworkCore;

public class UnidadAcademicaContext : DbContext
{
    public UnidadAcademicaContext(DbContextOptions<UnidadAcademicaContext> options)
        : base(options)
    {
    }

    public DbSet<Catedratico> Catedraticos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Catedratico>().ToTable("Tbl_Catedraticos");
    }
}
```

- **La Consulta LINQ**:
```
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public List<Catedratico> ObtenerCatedraticosModernos(UnidadAcademicaContext context)
{
    return context.Catedraticos
        .AsNoTracking()
        .Where(c => c.Nombre.StartsWith("Ing."))
        .ToList();
}
```

## Referencias Bibliográficas
- Facultad de Ingeniería, USAC. (2026). Sesión 17: Conectividad con SQL Server. Acceso Estructurado a Datos mediante C# y ADO.NET. Laboratorio de Introducción a la
Programación y Computación 2. Guatemala.

- Facultad de Ingeniería, USAC. (2026). Sesión 18: Mapeo de Objetos Relacionales. Persistencia Automatizada con Entity Framework Core. Laboratorio de Introducción a la Programación y Computación 2. Guatemala

