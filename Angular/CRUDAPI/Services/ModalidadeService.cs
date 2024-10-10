using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDAPI.Models;

namespace CRUDAPI.Services
{
    public class ModalidadeService
    {
        private readonly Contexto _contexto;

        public ModalidadeService(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Modalidade> ValidarModalidade(Modalidade modalidade)
        {
            if (string.IsNullOrWhiteSpace(modalidade.Nome))
            {
                throw new CampoObrigatorioException("O nome da modalidade é obrigatório.");
            }

            return modalidade;
        }

         public bool ModalidadeExists(long id)
        {
            return _contexto.Modalidades.Any(e => e.Id == id);
        }
    }
}
