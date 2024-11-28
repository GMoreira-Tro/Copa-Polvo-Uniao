using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Models;
using System.Net.Mail;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using CRUDAPI.Services;

namespace CRUDAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly UsuarioService _usuarioService;

        public UsuarioController(Contexto contexto, UsuarioService usuarioService)
        {
            _contexto = contexto;
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            try
            {
                if (Validators.ValidarSenha(usuario.SenhaHash))
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt(12);

                    // Hash da senha com o salt
                    usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash, salt);
                }
                usuario = await _usuarioService.ValidarUsuario(usuario);
                usuario.EmailConfirmado = false;
                usuario.TokenEmail = Validators.GerarTokenUnico(usuario.Email);

                // Se todas as validações passaram, salva o usuário no banco de dados
                _contexto.Usuarios.Add(usuario);
                await _contexto.SaveChangesAsync();
                await EnviarEmailConfirmacao(usuario.Email, usuario.TokenEmail);

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Método para enviar e-mail de confirmação
        [HttpPost("email-confirmation")]
        public async Task<IActionResult> EnviarEmailConfirmacao(string email, string token)
        {
            using (var client = new SmtpClient("live.smtp.mailtrap.io", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("api", "155e34e621d4d4a1b25bb7058e911616");

                // Configuração do e-mail
                var message = new MailMessage
                {
                    From = new MailAddress("suporte@seusite.com", "Equipe de Suporte"),
                    Subject = "Confirmação de Cadastro",
                    IsBodyHtml = true // Suporte a HTML para mensagens ricas
                };

                // Corpo do e-mail (HTML)
                string confirmationLink = "https://www.seusite.com/confirmacao?token={token}";
                message.Body = $@"
                    <html>
                        <body>
                            <p>Olá,</p>
                            <p>Obrigado por se cadastrar em nossa plataforma! Por favor, confirme seu cadastro clicando no link abaixo:</p>
                            <p><a href='{confirmationLink}'>Confirmar Cadastro</a></p>
                            <p>Se você não realizou esse cadastro, ignore este e-mail.</p>
                            <p>Atenciosamente,<br>Equipe de Suporte</p>
                        </body>
                    </html>";

                // Destinatário
                message.To.Add(email);

                // Envio do e-mail
                await client.SendMailAsync(message);
            }

            return Ok();
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(usuario).State = EntityState.Modified;

            try
            {
                usuario = await _usuarioService.ValidarUsuario(usuario);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_usuarioService.UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(long id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // Buscar o usuário pelo e-mail no banco de dados
            var user = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return Unauthorized(); // Usuário não encontrado
            }

            // Verificar se a senha fornecida corresponde ao hash no banco de dados
            if (BCrypt.Net.BCrypt.Verify(model.Senha, user.SenhaHash))
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes("k3ZvM4v9BpS+UdW7Y4XeFtHj2NsJ8bRa");

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7), // Token expira em 7 dias
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(new { Token = tokenString });
                }
                catch (Exception ex)
                {
                    // Log do erro
                    Console.WriteLine($"Erro ao gerar token JWT: {ex.Message}");
                    return StatusCode(500, "Erro interno no servidor ao gerar token JWT");
                }
            }
            else
            {
                return Unauthorized(); // Senha incorreta
            }
        }

    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}