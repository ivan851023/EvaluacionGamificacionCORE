using EvaluacionGamificacionCORE.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public interface IUsuario
    {

        void Insert(Usuario entity);

        Usuario GetById(int id);

        IEnumerable<Usuario> GetUsuarios();
    }
}
