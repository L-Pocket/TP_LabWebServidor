using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabAWS_RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {

        // Listado de funcionalidades mínimas evaluadas:

        // GET
        // Listar todos los productos pendientes de este tipo de empleado
        // Falta agregar FK de id_empleado a el Pedido

        // PUT
        // Debe cambiar el estado a “en preparación” y agregarle el tiempo de preparación (NUEVO ATRIBUTO DE ESTIMACION EN PEDIDO). apunta a EstadosPedido

        // PUT
        // Debe cambiar el estado de un pedido “listo para servir”. apunta a EstadosPedido

        // PUT
        // La moza se fija los pedidos que están listos para servir, cambia el estado de la mesa.
        // Descripción: apunta a EstadosMesa

        // PUT
        // La moza cobra la cuenta.
        // Descripción: apunta a EstadosMesa, cambia a "Cliente pagando".

        // PUT
        // Alguno de los socios cierra la mesa.
        // Descripción: apunta a EstadosMesa, cambia a "Cerrada".

        // GET 
        // Alguno de los socios pide el listado de pedidos y el tiempo de demora de ese pedido.

        // GET
        // Alguno de los socios pide el listado de las mesas y sus estados.


        //-----------------------------------------------------------------------------------------------
        // Requerimientos de la aplicación:
        // GET:
        //Los días y horarios que se Ingresaron al sistema los empleados.

        // GET
        //Cantidad de operaciones de todos por sector.
        //Descripción: listar empleados por sector. 

        // GET
        //Cantidad de operaciones de todos por sector, listada por cada empleado.
        // Descripción: listar productos por empleado.

        // POST
        //Posibilidad de dar de alta a nuevos empleados.

        // PUT
        //Posibilidad de suspender empleados
        // CREAR OTRO ATRIBUTO? ACTIVO/SUSPENDIDO? TIPO BOOL?

        //DELETE
        //Posibilidad de borrar empleados.

        // GET
        // Cantidad de operaciones de cada uno por separado.
        // Descripción: NO SE ENTIENDE

    }
}
