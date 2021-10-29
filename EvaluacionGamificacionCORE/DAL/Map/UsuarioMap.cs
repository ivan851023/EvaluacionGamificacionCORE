using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("USUARIO");
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Documento).HasColumnName("DOCUMENTO");
            entity.Property(e => e.Nombre_Completo).HasColumnName("NOMBRE_COMPLETO");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.Direccion).HasColumnName("DIRECCION");
            entity.Property(e => e.Fecha_Nacimiento).HasColumnName("FECHA_NACIMIENTO");
            entity.Property(e => e.Telefono).HasColumnName("TELEFONO");
            entity.Property(e => e.Password).HasColumnName("PASSWORD");
            entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");
        }

    }
}
