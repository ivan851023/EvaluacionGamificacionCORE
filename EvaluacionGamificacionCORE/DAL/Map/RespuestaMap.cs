using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Map
{
    public class RespuestaMap : IEntityTypeConfiguration<Respuestas>
    {
        public void Configure(EntityTypeBuilder<Respuestas> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("RESPUESTAS");
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Respuesta).HasColumnName("RESPUESTA");
            entity.Property(e => e.Id_Pregunta).HasColumnName("ID_PREGUNTA");
            entity.Property(e => e.Correcta).HasColumnName("CORRECTA");
            entity.Property(e => e.Incorrecta).HasColumnName("INCORRECTA");
            entity.HasOne(d => d.Preguntas)
                   .WithMany(p => p._respuestas)
                   .HasForeignKey(d => d.Id_Pregunta)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("PREGUNTAS_FK");
        }
    }
}
