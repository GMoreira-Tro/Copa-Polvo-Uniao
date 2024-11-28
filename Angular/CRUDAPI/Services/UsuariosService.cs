using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BCrypt.Net;

namespace CRUDAPI.Services
{
    public partial class UsuarioService
    {
        private readonly Contexto _contexto;

        public UsuarioService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                throw new CampoObrigatorioException("Nome");
            }

            if (string.IsNullOrEmpty(usuario.Sobrenome))
            {
                throw new CampoObrigatorioException("Sobrenome");
            }

            if (string.IsNullOrEmpty(usuario.Cpf))
            {
                throw new CampoObrigatorioException("Cpf");
            }

            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new CampoObrigatorioException("Email");
            }

            if(!Validators.IsValidEmail(usuario.Email))
            {
                throw new EmailInvalidoException();
            }

            var emailExistente = await _contexto.Usuarios.AnyAsync(u => u.Email == usuario.Email);
            if (emailExistente)
            {
                throw new EmailJaCadastradoException(); // Indica que o e-mail já está cadastrado
            }

            // Verifica se o CPF/CNPJ já está cadastrado
            var cpfExistente = await _contexto.Usuarios.AnyAsync(u => u.Cpf == usuario.Cpf);
            if (cpfExistente)
            {
                throw new CpfJaCadastradoException(); // Indica que o CPF/CNPJ já está cadastrado
            }

            // Valida o CPF/CNPJ
            if (!Validators.ValidarCPF(usuario.Cpf))
            {
                throw new CpfInvalidoException(); // Indica que o CPF/CNPJ é inválido
            }

            return usuario;
        }

        public bool UsuarioExists(long id)
        {
            return _contexto.Usuarios.Any(e => e.Id == id);
        }
    }
}
