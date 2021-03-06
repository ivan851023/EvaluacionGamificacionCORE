using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Map;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Context
{
    public class EvaluacionContext : DbContext
    {
        public EvaluacionContext()
        {

        }

        public EvaluacionContext(DbContextOptions<EvaluacionContext> options)
              : base(options)
        {
        }

        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<TipoMascota> TipoMascotas { get; set; }

        public DbSet<Puntuacion> Puntuaciones { get; set; }

        public DbSet<VwPuntuacion> VwPuntuaciones { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Preguntas> Preguntas { get; set; }

        public DbSet<Respuestas> Respuestas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new TipoMascotaMap());
            modelBuilder.ApplyConfiguration(new PuntuacionMap());
            modelBuilder.ApplyConfiguration(new VwPuntuacionMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PreguntaMap());
            modelBuilder.ApplyConfiguration(new RespuestaMap());
        }

    }
}
