using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Services
{
    public class InscricaoService
    {
        private readonly Contexto _contexto;
        public InscricaoService(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Inscricao> ValidarInscricao(Inscricao inscricao)
        {
            // Verifica se a categoria está definida
            if (inscricao.CategoriaId <= 0)
            {
                throw new CampoObrigatorioException("Categoria");
            }

            // Verifica se o PagamentoId já está associado a outra inscrição
            var pagamentoExistente = await _contexto.Inscricoes
                .AnyAsync(i => i.PagamentoId == inscricao.PagamentoId && i.Id != inscricao.Id);

            if (pagamentoExistente)
            {
                throw new InvalidOperationException($"O Pagamento com ID {inscricao.PagamentoId} já está associado a outra Inscrição.");
            }

            inscricao.ConfrontoInscricoes ??= new List<ConfrontoInscricao>();
            return inscricao;
        }


        public bool InscricaoExists(long id)
        {
            return _contexto.Inscricoes.Any(e => e.Id == id);
        }
    }
}
