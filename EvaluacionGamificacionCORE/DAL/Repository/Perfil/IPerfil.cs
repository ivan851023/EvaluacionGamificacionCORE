using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface IPerfil
    {
        void Insert(Perfil entity);

        Perfil GetById(int id);

        IEnumerable<Perfil> GetPerfiles();
    }
}
