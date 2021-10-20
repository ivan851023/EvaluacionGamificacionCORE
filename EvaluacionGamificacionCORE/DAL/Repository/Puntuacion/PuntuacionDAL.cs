using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public class PuntuacionDAL : IPuntuacion
    {
        private readonly IRepositoryBase<Puntuacion> _repository;
        public PuntuacionDAL(IRepositoryBase<Puntuacion> repository)
        {
            _repository = repository;
        }

        public void Insert(Puntuacion table)
        {
            _repository.Insert(table);
        }

        public Puntuacion GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Puntuacion> GetPuntuaciones()
        {
            return _repository.GetAll();
        }

    }
}
