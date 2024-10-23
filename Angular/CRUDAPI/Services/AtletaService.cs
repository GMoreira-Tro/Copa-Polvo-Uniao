using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDAPI.Models;

namespace CRUDAPI.Services
{
    public class AtletaService
    {
        private readonly Contexto _contexto;

        public AtletaService(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Atleta> ValidarAtleta(Atleta atleta)
        {
            if (string.IsNullOrWhiteSpace(atleta.Nome))
            {
                throw new CampoObrigatorioException("O nome do atleta é obrigatório.");
            }

            if (!Validators.ValidarCPF(atleta.Cpf))
            {
                throw new Exception("O CPF do atleta é inválido.");
            }

            return atleta;
        }

         public bool AtletaExists(long id)
        {
            return _contexto.Atletas.Any(e => e.Id == id);
        }
    }
}
