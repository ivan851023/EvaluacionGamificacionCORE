using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface IRespuestas
    {
        void Insert(Respuestas entity);

        Respuestas GetById(int id);

        IEnumerable<Respuestas> GetRespuestas();
    }
}
