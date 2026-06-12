# Actividad de Investigación y Práctica: Estructuras de Datos Avanzadas y APIs con ASP.NET Core

## Estructuras de Datos Eficientes

### Árboles Binarios de Búsqueda (ABB)

- Regla de ordenamiento: Para cada nodo, los valores en el subárbol izquierdo son menores y los del subárbol derecho son mayores.

- Principal desventaja: Si el orden de inserción es secuencial (1, 2, 3, 4, 5), el árbol se degenera en una lista enlazada, perdiendo eficiencia.

## Árboles AVL

- Definición: Un árbol AVL es un árbol binario de búsqueda autoequilibrado, donde la diferencia de altura entre los subárboles izquierdo y derecho de cualquier nodo no puede ser mayor que uno. Cada vez que se inserta o elimina un nodo, el árbol se reequilibra mediante rotaciones para mantener esta propiedad.

- Al asegurar que el árbol esté equilibrado, las operaciones de búsqueda, inserción y eliminación se mantienen estrictamente en O(log n).

## Fundamentos de Web APIs

### ¿Qué es una API y cómo funciona el modelo Client-Servidor?

- API (Interfaz de Programación de Aplicaciones): Es un conjunto de reglas y protocolos que permiten que diferentes aplicaciones se comuniquen entre sí.

- Modelo Cliente-Servidor: En este modelo, el cliente realiza solicitudes a un servidor, que procesa la solicitud y devuelve una respuesta. El cliente puede ser una aplicación web, móvil o de escritorio, mientras que el servidor aloja la API y maneja las solicitudes.

- Flujo a través de HTTP: 

    - El cliente envía una solicitud HTTP (GET, POST, PUT, DELETE) al servidor.
    - El servidor procesa la solicitud, accede a los datos necesarios.
    - El servidor devuelve un código de estado y una respuesta HTTP con el resultado.

### Verbos HTTP:

- GET: Solicita datos del servidor. No modifica el estado del servidor.

    - Uso correcto: Obtener una lista de usuarios, obtener detalles de un usuario específico, buscar productos.
    - Idempotencia: Sí, realizar la misma solicitud GET varias veces no cambia el estado del servidor.

- POST: Envía datos al servidor para crear un nuevo recurso. Puede modificar el estado del servidor.
    - Uso correcto: Crear un nuevo usuario, agregar un nuevo producto, enviar un formulario.
    - Idempotencia: No, realizar la misma solicitud POST varias veces puede crear múltiples recursos.

## Datos Estudiante

- Nombre: Josué Javier Carrera Soyós
- Carnet: 202300834
- Asignatura: IPC2 "P"
- Actividad: Investigación y Práctica: Estructuras de Datos Avanzadas y APIs con ASP.NET Core


