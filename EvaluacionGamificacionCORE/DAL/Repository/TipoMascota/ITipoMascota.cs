using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface ITipoMascota
    {
        void Insert(TipoMascota entity);

        TipoMascota GetById(int id);

        IEnumerable<TipoMascota> GetTipoMascotas();
    }
}
