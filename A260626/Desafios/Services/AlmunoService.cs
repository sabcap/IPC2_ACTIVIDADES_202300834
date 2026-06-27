using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Desafios.Models;
namespace Desafios.Services;

public class AlumnoService
{
    private readonly HttpClient _httpClient;

    public AlumnoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Alumno?> ObtenerAlumnoAsync()
    {
        const string url = "https://api.usac.edu/v1/alumnos";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Alumno? alumno = JsonSerializer.Deserialize<Alumno>(jsonResponse, options);
            return alumno;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error de comunicación con el servidor HTTP: {ex.Message}");
            throw; 
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error al deserializar la respuesta JSON: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            throw;
        }
    }
}