using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface IPuntuacion
    {
        void Insert(Puntuacion entity);

        Puntuacion GetById(int id);

        IEnumerable<Puntuacion> GetPuntuaciones();
    }
}
