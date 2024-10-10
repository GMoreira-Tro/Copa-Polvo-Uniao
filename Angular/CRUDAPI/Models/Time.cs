using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPI.Models
{
    public struct Atleta {
        public string nome;
        public string cpf;

        public Atleta(string nome, string cpf)
        {
            this.nome = nome;
            this.cpf = cpf;
        }
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
        public List<Atleta>? Atletas { get; set; }
    }
}
