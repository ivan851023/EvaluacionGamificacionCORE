using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public class VwPuntuacionDAL: IVwPuntuacion
    {
        private readonly IRepositoryBase<VwPuntuacion> _repository;
        public VwPuntuacionDAL(IRepositoryBase<VwPuntuacion> repository)
        {
            _repository = repository;
        }

        public void Insert(VwPuntuacion table)
        {
            _repository.Insert(table);
        }

        public VwPuntuacion GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<VwPuntuacion> GetDetallePuntuaciones()
        {
            return _repository.GetAll();
        }
    }
}
