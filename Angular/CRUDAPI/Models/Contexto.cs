using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Models
{
    /// <summary>
    /// Clase que contém todas as referências as tabelas do Banco de Dados bem como as definições dos relacionamentos.
    /// </summary>
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Modalidade> Modalidades { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Confronto> Confrontos { get; set; }
        public DbSet<ConfrontoInscricao> ConfrontoInscricoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<UsuarioNotificacao> UsuarioNotificacoes { get; set; }
        public DbSet<Premio> Premios { get; set; }
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }
        public DbSet<PagamentoContaCorrente> PagamentoContaCorrentes { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        /// <summary>
        /// Função que define o relacionamento entre as tabelas do Banco de Dados.
        /// </summary>
        /// <param name="modelBuilder">Construtor do modelo de relacionamento entre tabelas.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração das chaves estrangeiras e índices únicos

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseLazyLoadingProxies();
        // }

    }
}
