using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CRUDAPI.Models
{
    /// <summary>
    /// Inscrição referente a participação de um Time em uma Categoria da competição.
    /// </summary>
    [Table("Inscricoes")]
    public class Inscricao
    {
        [Key]
        public long Id { get; set; }
        
        [ForeignKey("Categorias")]
        public long CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        /// <summary>
        /// Id do Time a ser inscrito.
        /// </summary>
        [ForeignKey("Times")]
        public long TimeId { get; set; }
        /// <summary>
        /// Time a ser inscrito.
        /// </summary>
        public virtual Time? Time { get; set; }
        [ForeignKey("Pagamentos")]
        public long PagamentoId { get; set; }
        public virtual Pagamento? Pagamento { get; set; }

        /// <summary>
        /// Número da Posição após o término da participação da Inscrição. 
        /// 1º lugar, 2º lugar, etc...
        /// </summary>
        public int? Posição { get; set; }
        /// <summary>
        /// Indica se o inscrito deu WO.
        /// </summary>
        public bool WO { get; set; }
        /// <summary>
        /// Id do prêmio que essa Inscrição poderá resgatar ao final da sua participação na Competição.
        /// </summary>
        public long? PremioResgatavelId { get; set; }
        /// <summary>
        /// Prêmio que essa Inscrição poderá resgatar ao final da sua participação na Competição.
        /// </summary>
        public virtual Premio? PremioResgatavel { get; set; }
        public ICollection<ConfrontoInscricao> ConfrontoInscricoes { get; set; } = new List<ConfrontoInscricao>();
    }
}
