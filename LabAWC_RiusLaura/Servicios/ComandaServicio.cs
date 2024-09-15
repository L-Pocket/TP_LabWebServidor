using AutoMapper;
using Entidades;
using LabAWC_RiusLaura.DAL.Data;
using LabAWS_RiusLaura.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabAWS_RiusLaura.Servicios
{
    public interface IComandaServicio
    {


        Task<(bool Success, string ErrorMessage, ComandaDto NuevaComanda)> CrearComanda(ComandaDto comandaDto);
        Task<(bool Success, string ErrorMessage, List<ComandaDto> Comandas)> ObtenerTodasLasComandas();
        Task<(bool Success, string ErrorMessage, ComandaDto? Comanda)> ObtenerComandaPorId(int id);
        Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaDto comandaDto);
        /*
                Task<(bool Success, string ErrorMessage, Comanda NuevaComanda)> CrearComanda(ComandaDto comandaDto);
                Task<(bool Success, string ErrorMessage, List<Comanda> Comandas)> ObtenerTodasLasComandas();
                Task<(bool Success, string ErrorMessage, Comanda? Comanda)> ObtenerComandaPorId(int id);
                Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaDto comandaDto);
        */


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


        public async Task<(bool Success, string ErrorMessage, ComandaDto NuevaComanda)> CrearComanda(ComandaDto comandaDto)
        {
            try
            {
                var mesa = await _context.Mesas.FindAsync(comandaDto.MesaDeComandaId);

                if (mesa == null)
                {
                    return (false, $"No se encontró una Mesa con el id {comandaDto.MesaDeComandaId}.", null);
                }

                var comanda = mapper.Map<Comanda>(comandaDto);

                _context.Comandas.Add(comanda);
                await _context.SaveChangesAsync();

                var comandaDtoResult = mapper.Map<ComandaDto>(comanda); // Mapear la entidad Comanda a ComandaDto

                return (true, null, comandaDtoResult);
            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}", null);
            }
        }

        /*
                    try
                    {


                        // Verificar si la mesa existe
                        var mesa = await _context.Mesas.FindAsync(comandaDto.MesaDeComandaId);

                        if (mesa == null)
                        {
                            return (false, $"No se encontró una Mesa con el id {comandaDto.MesaDeComandaId}.", null);
                        }

                        var comanda = new Comanda
                        {
                            MesaDeComandaId = comandaDto.MesaDeComandaId,
                            NombreCliente = comandaDto.NombreCliente
                        };

                        _context.Comandas.Add(comanda);
                        await _context.SaveChangesAsync();

                        return (true, null, comanda);
                    }
                    catch (Exception ex)
                    {
                        return (false, $"Error interno del servidor: {ex.Message}", null);
                    }

                }
        */

        public async Task<(bool Success, string ErrorMessage, List<ComandaDto> Comandas)> ObtenerTodasLasComandas()
        {
            try
            {
                var comandas = await _context.Comandas
                    .Include(c => c.MesaDeComanda)
                    .ToListAsync();

                // Mapeamos la lista de entidades Comanda a ComandaDto
                var comandasDto = mapper.Map<List<ComandaDto>>(comandas);

                return (true, null, comandasDto);
            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}", null);
            }
        }




        /*
        public async Task<(bool Success, string ErrorMessage, List<Comanda> Comandas)> ObtenerTodasLasComandas()
        {
            try
            {
                var comandas = await _context.Comandas
                    .Include(c => c.MesaDeComanda)
                    .ToListAsync();

                return (true, null, comandas);
            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}", null);
            }
        }

        */



        public async Task<(bool Success, string ErrorMessage, ComandaDto? Comanda)> ObtenerComandaPorId(int id)
        {
            try
            {
                var comanda = await _context.Comandas
                    .Include(c => c.MesaDeComanda)
                    .FirstOrDefaultAsync(c => c.IdComanda == id);

                if (comanda == null)
                {
                    return (false, $"No se encontró una comanda con el id {id}.", null);
                }

                // Mapeamos la entidad Comanda a ComandaDto
                var comandaDto = mapper.Map<ComandaDto>(comanda);

                return (true, null, comandaDto);
            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}", null);
            }
        }


        /*
                public async Task<(bool Success, string ErrorMessage, Comanda? Comanda)> ObtenerComandaPorId(int id)
                {
                    try
                    {
                        var comanda = await _context.Comandas
                            .Include(c => c.MesaDeComanda)
                            .FirstOrDefaultAsync(c => c.IdComanda == id);

                        if (comanda == null)
                        {
                            return (false, $"No se encontró una comanda con el id {id}.", null);
                        }

                        return (true, null, comanda);
                    }
                    catch (Exception ex)
                    {
                        return (false, $"Error interno del servidor: {ex.Message}", null);
                    }
                }

         */

        public async Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaDto comandaDto)
        {
            try
            {
                // Verificar si la comanda existe
                var comanda = await _context.Comandas.FindAsync(idComanda);

                if (comanda == null)
                {
                    return (false, $"No se encontró una comanda con el id {idComanda}.");
                }

                // Verificar si la mesa existe
                var mesa = await _context.Mesas.FindAsync(comandaDto.MesaDeComandaId);

                if (mesa == null)
                {
                    return (false, $"No se encontró una Mesa con el id {comandaDto.MesaDeComandaId}.");
                }

                // Mapeamos el DTO sobre la entidad existente
                mapper.Map(comandaDto, comanda);

                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}");
            }
        }




        /*
        public async Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaDto comandaDto)
        {
            try
            {
                // Verificar si la comanda existe
                var comanda = await _context.Comandas.FindAsync(idComanda);

                if (comanda == null)
                {
                    return (false, $"No se encontró una comanda con el id {idComanda}.");
                }

                // Verificar si la mesa existe
                var mesa = await _context.Mesas.FindAsync(comandaDto.MesaDeComandaId);

                if (mesa == null)
                {
                    return (false, $"No se encontró una Mesa con el id {comandaDto.MesaDeComandaId}.");
                }

                // Actualizar la comanda
                comanda.MesaDeComandaId = comandaDto.MesaDeComandaId;
                comanda.NombreCliente = comandaDto.NombreCliente;

                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"Error interno del servidor: {ex.Message}");
            }
        }
        */

    }
}

