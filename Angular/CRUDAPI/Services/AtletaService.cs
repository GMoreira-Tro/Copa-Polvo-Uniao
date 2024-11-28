using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            if(!Validators.ValidarRG(atleta.Rg))
            {
                throw new Exception("O RG do atleta é inválido.");
            }
            // Verifica se o CPF já está cadastrado
            var atletaComMesmoCpf = await _contexto.Atletas.FirstAsync(a => a.Cpf == atleta.Cpf);
            if (atletaComMesmoCpf != null)
            {
                throw new Exception("Já existe um atleta cadastrado com este CPF.");
            }

            // Verifica se o RG já está cadastrado
            var atletaComMesmoRg = await _contexto.Atletas.FirstAsync(a => a.Rg == atleta.Rg);
            if (atletaComMesmoRg != null)
            {
                throw new Exception("Já existe um atleta cadastrado com este RG.");
            }

            return atleta;
        }

         public bool AtletaExists(long id)
        {
            return _contexto.Atletas.Any(e => e.Id == id);
        }
    }
}
