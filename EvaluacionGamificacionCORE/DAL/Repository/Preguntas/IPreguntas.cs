using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface IPreguntas
    {
        void Insert(Preguntas entity);

        Preguntas GetById(int id);

        IEnumerable<Preguntas> GetPreguntas();
    }
}
