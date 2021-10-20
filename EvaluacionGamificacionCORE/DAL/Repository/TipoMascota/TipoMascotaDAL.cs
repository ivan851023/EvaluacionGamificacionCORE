using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public class TipoMascotaDAL : ITipoMascota
    {

        private readonly IRepositoryBase<TipoMascota> _repository;
        public TipoMascotaDAL(IRepositoryBase<TipoMascota> repository)
        {
            _repository = repository;
        }

        public void Insert(TipoMascota table)
        {
            _repository.Insert(table);
        }

        public TipoMascota GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<TipoMascota> GetTipoMascotas()
        {
            return _repository.GetAll();
        }

    }
}
