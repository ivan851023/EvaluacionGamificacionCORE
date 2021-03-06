using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Map
{
    public class VwPuntuacionMap : IEntityTypeConfiguration<VwPuntuacion>
    {
        public void Configure(EntityTypeBuilder<VwPuntuacion> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToView("VW_PUNTUACION");
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Perfil).HasColumnName("PERFIL");
            entity.Property(e => e.TipoMascota).HasColumnName("TIPO_MASCOTA");
            entity.Property(e => e.Documento).HasColumnName("DOCUMENTO");
            entity.Property(e => e.Nombre_Completo).HasColumnName("NOMBRE_COMPLETO");
            entity.Property(e => e.Email).HasColumnName("EMAIL");
            entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");
            entity.Property(e => e.Puntaje).HasColumnName("PUNTAJE");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

        }
    }
}
