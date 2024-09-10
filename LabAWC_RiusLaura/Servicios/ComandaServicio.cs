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
        Task<(bool Success, string ErrorMessage, Comanda NuevaComanda)> CrearComanda(ComandaDto comandaDto);
        Task<(bool Success, string ErrorMessage, List<Comanda> Comandas)> ObtenerTodasLasComandas();
        Task<(bool Success, string ErrorMessage, Comanda? Comanda)> ObtenerComandaPorId(int id);
        Task<(bool Success, string ErrorMessage)> ModificarComanda(int idComanda, ComandaDto comandaDto);
    }

    public class ComandaServicio : IComandaServicio
    {
        private readonly DataContext _context;

        public ComandaServicio(DataContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string ErrorMessage, Comanda NuevaComanda)> CrearComanda(ComandaDto comandaDto)
        {
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

    }
}
