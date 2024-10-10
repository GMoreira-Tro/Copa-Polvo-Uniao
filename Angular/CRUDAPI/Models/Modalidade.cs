using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPI.Models
{
    /// <summary>
    /// Modalidades esportivas da Copa.
    /// </summary>
    [Table("Modalidades")]
    public class Modalidade
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; } = "";
        public string? Descricao { get; set; }
        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();
    }
}
