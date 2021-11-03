using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public class RespuestasDAL : IRespuestas
    {
        private readonly IRepositoryBase<Respuestas> _repository;
        public RespuestasDAL(IRepositoryBase<Respuestas> repository)
        {
            _repository = repository;
        }

        public void Insert(Respuestas table)
        {
            _repository.Insert(table);
        }

        public Respuestas GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Respuestas> GetRespuestas()
        {
            return _repository.GetAll();
        }
    }
}
