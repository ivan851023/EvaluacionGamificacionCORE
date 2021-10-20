﻿using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Map
{
    public class PuntuacionMap : IEntityTypeConfiguration<Puntuacion>
    {
        public void Configure(EntityTypeBuilder<Puntuacion> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("PUNTUACION");
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdPerfil).HasColumnName("ID_PERFIL");
            entity.HasAlternateKey(e => e.IdPerfil);
            entity.Property(e => e.IdTipoMascota).HasColumnName("ID_TIPO_MASCOTA");
            entity.HasAlternateKey(e => e.IdTipoMascota);
            entity.Property(e => e.NumeroDocumento).HasColumnName("NUMERO_DOCUMENTO");
            entity.Property(e => e.FechaCreacion).HasColumnName("FECHA_CREACION");
            entity.Property(e => e.Puntaje).HasColumnName("PUNTAJE");

        }
    }
}
