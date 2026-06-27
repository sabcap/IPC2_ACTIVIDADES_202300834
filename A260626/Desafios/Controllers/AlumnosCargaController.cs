using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Desafios.Models;
using Desafios.Data;
namespace Desafios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnosCargaController : ControllerBase
{
    private readonly MyDbContext _context; // Data/MyDbContext 

    public AlumnosCargaController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost("cargar-masivo")]
    public async Task<IActionResult> CargarCsvMasivo(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
        {
            return BadRequest("El archivo no es válido o está vacío.");
        }

        if (!Path.GetExtension(archivo.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Solo se permiten archivos en formato .csv");
        }

        try
        {
        
            var loteAlumnos = new List<Alumno>();    
            
            const int batchSize = 1000; 
            
            using (var stream = archivo.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string? linea;
                bool esPrimeraLinea = true;

                
                while ((linea = await reader.ReadLineAsync()) != null)
                {
                    
                    if (esPrimeraLinea)
                    {
                        esPrimeraLinea = false;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    
                    string[] columnas = linea.Split(',');
                    
                    var nuevoAlumno = new Alumno
                    {
                        
                        Carnet = columnas[0].Trim(),
                        Nombre = columnas[1].Trim()
                    };

                    loteAlumnos.Add(nuevoAlumno);

                    
                    if (loteAlumnos.Count >= batchSize)
                    {
                        await _context.Alumnos.AddRangeAsync(loteAlumnos);
                        loteAlumnos.Clear(); // 
                    }
                }
            }

            
            if (loteAlumnos.Count > 0)
            {
                await _context.Alumnos.AddRangeAsync(loteAlumnos);
            }

            
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Carga masiva procesada y almacenada con éxito." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { error = $"Ocurrió un error al procesar el archivo: {ex.Message}" });
        }
    }
}