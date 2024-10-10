using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDAPI.Models;

namespace CRUDAPI.Services
{
    public class TimeService
    {
        private readonly Contexto _contexto;

        public TimeService(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Time> ValidarTime(Time time)
        {
            if (string.IsNullOrWhiteSpace(time.Nome))
            {
                throw new CampoObrigatorioException("O nome do time é obrigatório.");
            }

            return time;
        }

         public bool TimeExists(long id)
        {
            return _contexto.Times.Any(e => e.Id == id);
        }
    }
}
