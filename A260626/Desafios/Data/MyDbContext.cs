using Microsoft.EntityFrameworkCore;

using Desafios.Models;
namespace Desafios.Data;

public class MyDbContext : DbContext
{
    // El constructor es indispensable para que ASP.NET Core le pase la configuración
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    // representa la tabla "Alumnos" en la base de datos
    public DbSet<Alumno> Alumnos { get; set; }
}