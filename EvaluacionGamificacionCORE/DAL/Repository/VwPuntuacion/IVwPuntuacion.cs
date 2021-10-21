using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface IVwPuntuacion
    {
        void Insert(VwPuntuacion entity);

        VwPuntuacion GetById(int id);

        IEnumerable<VwPuntuacion> GetDetallePuntuaciones();
    }
}
