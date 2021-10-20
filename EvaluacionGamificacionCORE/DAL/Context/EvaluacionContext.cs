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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new TipoMascotaMap());

        }

    }
}
