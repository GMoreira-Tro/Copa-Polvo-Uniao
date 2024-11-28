using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CRUDAPI.Models
{
    public enum Role
    {
        Admin,
        Cliente
    }
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; } = "";
        [Required]
        public string Sobrenome { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        /// <summary>
        /// Indica se o usuário confirmou seu cadastro.
        /// </summary>
        public bool EmailConfirmado { get; set; } = false;
        public string TokenEmail { get; set; } = "";

        /// <summary>
        /// Senha salva após encriptação.
        /// </summary>
        [Required]
        public string SenhaHash { get; set; } = "";

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string Cpf { get; set; } = "";

        /// <summary>
        /// Permissão de acesso do Usuário.
        /// </summary>
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        public virtual ICollection<UsuarioNotificacao> UsuarioNotificacaos { get; set; } = new List<UsuarioNotificacao>();
    }
}