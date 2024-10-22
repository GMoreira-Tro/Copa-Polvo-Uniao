using System;
using System.Linq;
using System.Threading.Tasks;
using CRUDAPI.Models;

namespace CRUDAPI.Services
{
    public class PagamentoContaCorrenteService
    {
        private readonly Contexto _contexto;

        public PagamentoContaCorrenteService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<PagamentoContaCorrente> ValidarPagamentoContaCorrente(PagamentoContaCorrente pagamentoContaCorrente)
        {
            //TODO: Implemente a lógica de validação do pagamentoContaCorrente de inscrição aqui, se necessário

            return pagamentoContaCorrente;
        }

        public bool PagamentoContaCorrenteExists(long id)
        {
            return _contexto.PagamentoContaCorrentes.Any(pi => pi.Id == id);
        }
    }
}
