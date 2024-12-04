using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;
using Restaurante_API.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IComandaServicio
    {


        
        Task<(bool Success, string ErrorMessage, ComandaDto NuevaComanda)> CrearComanda(ComandaCrearDto comandaCrearDto);
        Task<(bool Success, string ErrorMessage, List<ComandaDto> Comandas)> ObtenerTodasLasComandas();
        Task<(bool Success, string ErrorMessage, ComandaDto? Comanda)> ObtenerComandaPorId(int id);
        
        Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaCrearDto comandaCrearDto);



       


    }

    public class ComandaServicio : IComandaServicio
    {
        private readonly DataContext _context;
        private readonly ILogger<ComandaServicio> logger;
        private readonly IMapper mapper;

        public ComandaServicio(DataContext context, ILogger<ComandaServicio> logger, IMapper mapper)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));//se lanza una excepción, lo que asegura que el servicio no intente operar con una dependencia no válida.
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<(bool Success, string ErrorMessage, ComandaDto NuevaComanda)> CrearComanda(ComandaCrearDto comandaCrearDto)
        {
            try
            {
                this.logger.LogInformation("Iniciando creación de comanda para la mesa ID: {MesaDeComandaId}", comandaCrearDto.MesaDeComandaId);

                var mesa = await _context.Mesas.FindAsync(comandaCrearDto.MesaDeComandaId);

                if (mesa == null)
                {
                    this.logger.LogWarning("No se encontró una mesa con el id: {MesaDeComandaId}", comandaCrearDto.MesaDeComandaId);
                    return (false, $"No se encontró una Mesa con el id {comandaCrearDto.MesaDeComandaId}.",null);
                }

                var comanda = this.mapper.Map<Comanda>(comandaCrearDto);

                _context.Comandas.Add(comanda);
                await _context.SaveChangesAsync();

                var comandaDtoResult = this.mapper.Map<ComandaDto>(comanda); // Mapear la entidad Comanda a ComandaDto

                this.logger.LogInformation("Comanda creada exitosamente con ID: {IdComanda}", comandaDtoResult.IdComanda);
                //return (true, null, comandaDtoResult);
                return (true, "", comandaDtoResult);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error al crear la comanda para la mesa ID: {MesaDeComandaId}", comandaCrearDto.MesaDeComandaId);
               // return (false, $"Error interno del servidor: {ex.Message}", null);
                return (false, $"Error interno del servidor: {ex.Message}", new ComandaDto());
            }
        }


        public async Task<(bool Success, string ErrorMessage, List<ComandaDto> Comandas)> ObtenerTodasLasComandas()
        {
            this.logger.LogInformation("Iniciando ObtenerTodasLasComandas");

            try
            {
                var comandas = await _context.Comandas
                    .Include(c => c.MesaDeComanda)
                    .ToListAsync();

                var comandasDto = this.mapper.Map<List<ComandaDto>>(comandas);

                this.logger.LogInformation("Obtenidas {Count} comandas", comandasDto.Count);

                return (true, "", comandasDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error al obtener todas las comandas");
                 return (false, $"Error interno del servidor: {ex.Message}", null);
                
            }
        }

        public async Task<(bool Success, string ErrorMessage, ComandaDto? Comanda)> ObtenerComandaPorId(int id)
        {
            this.logger.LogInformation("Iniciando ObtenerComandaPorId con Id: {Id}", id);

            try
            {
                var comanda = await _context.Comandas
                    .Include(c => c.MesaDeComanda)
                    .FirstOrDefaultAsync(c => c.IdComanda == id);

                if (comanda == null)
                {
                    string mensaje = $"No se encontró una comanda con el id {id}.";
                    this.logger.LogWarning(mensaje);
                    return (false, mensaje, null);
                }

                var comandaDto = this.mapper.Map<ComandaDto>(comanda);

                this.logger.LogInformation("Comanda obtenida exitosamente con Id: {IdComanda}", comandaDto.IdComanda);

                return (true, "", comandaDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error al obtener la comanda por Id");
                return (false, $"Error interno del servidor: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaCrearDto comandaCrearDto)
        {
            this.logger.LogInformation("Iniciando ModificarComanda con IdComanda: {IdComanda}", idComanda);

            try
            {
                var comanda = await _context.Comandas.FindAsync(idComanda);

                if (comanda == null)
                {
                    string mensaje = $"No se encontró una comanda con el id {idComanda}.";
                    this.logger.LogWarning(mensaje);
                    return (false, mensaje);
                }

                // Validar la existencia de la mesa solo si es necesario modificar la mesa asociada a la comanda
                if (comandaCrearDto.MesaDeComandaId != comanda.MesaDeComandaId)
                {
                    var mesa = await _context.Mesas.FindAsync(comandaCrearDto.MesaDeComandaId);

                    if (mesa == null)
                    {
                        string mensaje = $"No se encontró una Mesa con el id {comandaCrearDto.MesaDeComandaId}.";
                        this.logger.LogWarning(mensaje);
                        return (false, mensaje);
                    }
                }

                // Mapear los datos del DTO a la entidad de Comanda
                comanda.MesaDeComandaId = comandaCrearDto.MesaDeComandaId;
                comanda.NombreCliente = comandaCrearDto.NombreCliente;

                await _context.SaveChangesAsync();

                this.logger.LogInformation("Comanda modificada exitosamente con Id: {IdComanda}", idComanda);
                return (true,"");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error al modificar la comanda");
                return (false, $"Error interno del servidor: {ex.Message}");
            }
        }
    }



    

    
    

    

    

    
   


    

}


