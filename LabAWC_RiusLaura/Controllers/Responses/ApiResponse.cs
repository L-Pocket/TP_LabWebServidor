namespace Restaurante_API.Controllers.Responses
{
    // Clase genérica para estandarizar respuestas de endpoints
    public class ApiResponse<T>
    {
        public bool Success { get; set; } // Si la operación fue exitosa o no, true o false
        public string Message { get; set; } // Agregar un mensaje personalizado.
        public T Data { get; set; } // El tipo T para que sea reutilizable con diferentes tipos de datos.
    }
}
