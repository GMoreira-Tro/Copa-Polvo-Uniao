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
        public DbSet<Atleta> Atletas { get; set; }
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
             // Relacionamento 1: Pagamento -> PagamentoContasCorrente
            modelBuilder.Entity<Pagamento>()
                .HasMany(p => p.PagamentoContasCorrente)
                .WithOne(pcc => pcc.Pagamento)
                .HasForeignKey(pcc => pcc.PagamentoId) // Definindo a FK explicitamente
                .OnDelete(DeleteBehavior.Restrict);    // Ajustando o comportamento da exclusão

            // Relacionamento 2: PagamentoContaCorrente -> Pagamento
            modelBuilder.Entity<PagamentoContaCorrente>()
                .HasOne(pcc => pcc.Pagamento)
                .WithMany(p => p.PagamentoContasCorrente)
                .HasForeignKey(pcc => pcc.PagamentoId) // Definindo a FK explicitamente
                .OnDelete(DeleteBehavior.Restrict);    // Ajustando o comportamento da exclusão

             modelBuilder.Entity<Usuario>()
                .HasMany(u => u.UsuarioNotificacaos)
                .WithOne(un => un.Usuario)
                .HasForeignKey(un => un.UsuarioId) // Definindo a FK explicitamente
                .OnDelete(DeleteBehavior.Restrict);    // Ajustando o comportamento da exclusão
                // Configuração de chave estrangeira entre UsuarioNotificacoes e Usuarios
            modelBuilder.Entity<UsuarioNotificacao>()
                .HasOne(un => un.Usuario)
                .WithMany(u => u.UsuarioNotificacaos)
                .HasForeignKey(un => un.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); // Impede exclusão em cascata

            modelBuilder.Entity<ContaCorrente>(entity =>
                {
                    // Especifica precisão e escala para a coluna Saldo
                    entity.Property(c => c.Saldo)
                        .HasPrecision(18, 2); // 18 dígitos no total, 2 casas decimais
                });
            modelBuilder.Entity<Inscricao>()
                .HasOne(i => i.Time)
                .WithMany(t => t.Inscricoes)
                .HasForeignKey(i => i.TimeId)
                .OnDelete(DeleteBehavior.Restrict); // ou .OnDelete(DeleteBehavior.NoAction);
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseLazyLoadingProxies();
        // }

    }
}
