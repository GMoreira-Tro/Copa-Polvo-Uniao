using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPI.Models
{
    /// <summary>
    /// Categoria associada a uma Modalidade onde os Times serão inscritos. Exemplos de Nomes:
    /// Feminino Juvenil, Sub-21 Masculino
    /// </summary>
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Exemplos de Nomes: Sênior Masculino,
        /// Feminino Juvenil, Sub-21 Masculino
        /// </summary>
        [Required]
        public string Nome { get; set; } = "";
        public string? Descricao { get; set; }
        [ForeignKey("Modalidades")]
        public long ModalidadeId { get; set; }
        public virtual Modalidade? Modalidade { get; set; }
        public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
    }
}
