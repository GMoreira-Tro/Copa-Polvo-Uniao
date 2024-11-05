using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPI.Models
{
    [Table("Atletas")]
    public class Atleta {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; } = "";
        [Required]
        public string Cpf { get; set; } = "";
    }
    /// <summary>
    /// Times inscritos na Copa.
    /// </summary>
    [Table("Times")]
    public class Time
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; } = "";
        public string Municipio { get; set; } = "";
        [ForeignKey("Usuarios")]
        public long UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public List<Atleta>? Atletas { get; set; }
    }
}
