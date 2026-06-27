# Actividad de Laboratorio: Interoperabilidad y Carga Masiva de Datos

## Parte 1: Evaluación Conceptual y Buenas Prácticas

### Formatos de Intercambio

| Formato | Ventajas | Desventajas |
|---------|----------|-------------|
| CSV    | - Fácil de leer y escribir. <br> - Compatible con muchas aplicaciones. | - No soporta estructuras jerárquicas. <br> - No incluye metadatos. |
| JSON   | - Ligero y fácil de parsear. <br> - Soporta estructuras jerárquicas. | - Puede ser más difícil de leer para humanos en grandes volúmenes. |

### Diferenciación de Procesos

- **Serialización `(JsonSerializer.Serialize)`**: Es el proceso de convertir un objeto en memoria (una instancia de una clase de C#) en una cadena de texto en formato JSON (o un flujo de bytes). Se usa para enviar o guardar los datos.

- **Deserialización `(JsonSerializer.Deserialize)`**: Es el proceso inverso; toma una cadena de texto JSON (o un flujo de bytes) y reconstruye la estructura para transformarla de vuelta en un objeto fuertemente tipado en la memoria de la aplicación. Se usa para recibir o leer los datos.


### El Antipatrón de Rendimiento

Ocurre cuando, al leer un archivo masivo, el sistema realiza 1 consulta inicial para obtener una lista de registros principales y luego ejecuta N consultas individuales adicionales (una por cada registro) en la base de datos para obtener o guardar sus datos relacionados. Si el archivo tiene 10,000 filas, se terminarán haciendo 10,001 peticiones a la base de datos, lo que destruye el rendimiento debido a la latencia de red (network roundtrips).

**Estrategia de Optimización**:
Para solucionarlo, en lugar de procesar y guardar los registros uno a uno, se debe aplicar **Batching**:

- Se van acumulando los registros leídos del archivo en memoria dentro de un bloque o lote con un tamaño controlado (por ejemplo, en grupos de 500 o 1,000 registros).

- Una vez que el lote se llena, se realiza una sola operación masiva (como un **InsertRange** o una consulta con un operador **IN** con todos los identificadores del lote) hacia la base de datos.

- Esto reduce drásticamente las conexiones, transformando las miles de peticiones individuales en un puñado de operaciones por bloques mucho más eficientes.


## Parte 2: Implementación Práctica en C#

Código del área practica implementado en C# almacenado en la carpeta `Desafios`.

## Referencias Bibliográficas

- **Facultad de Ingeniería, USAC. (2026)**. Sesión 20: Integración de Datos.Consumo de APIs Externas y Carga Masiva (CSV/XML). Laboratorio del curso Introducción a la Programación y Computación 2. Guatemala.

