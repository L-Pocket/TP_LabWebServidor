﻿using Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LabAWS_RestauranteAPI.DAL.DataSeed
{
    public class RolSeed : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasData(
            new Rol
            {
                IdRol = 1,
                DescripcionRol = "Bartender"
            },
            new Rol
            {
                IdRol = 2,
                DescripcionRol = "Cervecero"
            },
            new Rol
            {
                IdRol = 3,
                DescripcionRol = "Cocinero"
            },
            new Rol
            {
                IdRol = 4,
                DescripcionRol = "Mozo"
            },
            new Rol
            {
                IdRol = 5,
                DescripcionRol = "Socio"
            }
            );
        }
    }
}
