using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Repository;

namespace EvaluacionGamificacionCORE
{
    public class PerfilDAL : IPerfil
    {
        private readonly IRepositoryBase<Perfil> _repository;
        public PerfilDAL(IRepositoryBase<Perfil> repository)
        {
            _repository = repository;
        }

        public void Insert(Perfil table)
        {
            _repository.Insert(table);
        }

        public Perfil GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Perfil> GetPerfiles()
        {
            return _repository.GetAll();
        }

    }
}
