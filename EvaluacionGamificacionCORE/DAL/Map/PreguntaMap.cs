using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Map
{
    public class PreguntaMap : IEntityTypeConfiguration<Preguntas>
    {
        public void Configure(EntityTypeBuilder<Preguntas> entity)
        {
            entity.HasKey(e => e.id);
            entity.ToTable("PREGUNTAS");
            entity.HasIndex(e => e.id);
            entity.Property(e => e.id).HasColumnName("ID");
            entity.Property(e => e.Pregunta).HasColumnName("PREGUNTA");
            entity.Property(e => e.IdPerfil).HasColumnName("ID_PERFIL");
            entity.Property(e => e.IdTipoMascota).HasColumnName("ID_TIPO_MASCOTA");
            //entity.HasMany(e => e._respuestas).WithOne(e => e.Preguntas).HasForeignKey(x => x.Id_Pregunta);
            
            //entity.HasOne(x => x.Perfil).WithMany(e => e.Puntuaciones).HasForeignKey(x => x.IdPerfil);
        }
    }
}
