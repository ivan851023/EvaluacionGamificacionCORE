using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Map
{
    public class TipoMascotaMap : IEntityTypeConfiguration<TipoMascota>
    {

        public void Configure(EntityTypeBuilder<TipoMascota> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("TIPO_MASCOTA");
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasColumnName("NOMBRE");

        }
    }
}
