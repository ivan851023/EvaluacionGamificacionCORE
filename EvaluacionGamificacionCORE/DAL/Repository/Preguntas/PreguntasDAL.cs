using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE
{
    public class PreguntasDAL : IPreguntas
    {
        private readonly IRepositoryBase<Preguntas> _repository;
        public PreguntasDAL(IRepositoryBase<Preguntas> repository)
        {
            _repository = repository;
        }

        public void Insert(Preguntas table)
        {
            _repository.Insert(table);
        }

        public Preguntas GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Preguntas> GetPreguntas()
        {
            return _repository.GetAll();
        }
    }
}
