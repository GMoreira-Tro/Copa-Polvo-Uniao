using CRUDAPI.Models;
using CRUDAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CRUDAPI {
    public static class DbInitializer
    {
        public static void InicializarBancoDados(Contexto context)
        {
            // Deleta o banco e recria do zero
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Popula o banco com dados de exemplo
            InserirDadosMockados(context);
        }

        private static void InserirDadosMockados(Contexto context)
        {
            InserirModalidades(context);
            InserirCategorias(context);
            InserirUsuarios(context);
            InserirAtletasETimes(context);
            InserirContasCorrentes(context);
            InserirPagamentos(context);
        }

        private static void InserirModalidades(Contexto context)
        {
            // Verifica se já existem modalidades
            if (context.Modalidades.Any())
            {
                return; // Sai se já existirem dados
            }

            // Cria uma lista de modalidades
            var modalidades = new List<Modalidade>
            {
                new Modalidade { Nome = "Futsal", Descricao = "Modalidade de futebol jogada em um campo coberto." },
                new Modalidade { Nome = "Vôlei", Descricao = "Modalidade de esporte em equipe jogada com uma bola." },
                new Modalidade { Nome = "Basquete", Descricao = "Modalidade de esporte em equipe jogada em uma quadra." },
                new Modalidade { Nome = "Judô", Descricao = "Arte marcial de origem japonesa focada em lançamentos." },
                new Modalidade { Nome = "Natação", Descricao = "Esporte aquático que envolve o movimento através da água." },
                new Modalidade { Nome = "Atletismo", Descricao = "Modalidade que inclui corridas, saltos e lançamentos." },
                new Modalidade { Nome = "Tênis", Descricao = "Esporte jogado em uma quadra com raquete e bola." },
                new Modalidade { Nome = "Ciclismo", Descricao = "Modalidade de esportes que envolve a bicicleta." }
                // Adicione mais modalidades conforme necessário
            };

            // Adiciona as modalidades ao contexto
            context.Modalidades.AddRange(modalidades);

            // Salva as alterações no banco de dados
            context.SaveChanges();
        }
        private static void InserirCategorias(Contexto context)
        {
            // Verifica se já existem categorias
            if (context.Categorias.Any())
            {
                return; // Sai se já existirem dados
            }

            // Obtém as modalidades existentes
            var modalidades = context.Modalidades.ToList();

            // Cria uma lista de categorias
            var categorias = new List<Categoria>
            {
                new Categoria { Nome = "Sênior Masculino", Descricao = "Categoria para homens acima de 18 anos", ModalidadeId = modalidades.First(m => m.Nome == "Futsal").Id },
                new Categoria { Nome = "Feminino Juvenil", Descricao = "Categoria para mulheres de 14 a 17 anos", ModalidadeId = modalidades.First(m => m.Nome == "Vôlei").Id },
                new Categoria { Nome = "Sub-21 Masculino", Descricao = "Categoria para homens de 18 a 21 anos", ModalidadeId = modalidades.First(m => m.Nome == "Basquete").Id },
                new Categoria { Nome = "Judô - Iniciante", Descricao = "Categoria para iniciantes no judô", ModalidadeId = modalidades.First(m => m.Nome == "Judô").Id },
                new Categoria { Nome = "Natação - Livre", Descricao = "Categoria livre para natação", ModalidadeId = modalidades.First(m => m.Nome == "Natação").Id },
                new Categoria { Nome = "Atletismo - 100m", Descricao = "Categoria para corrida de 100 metros", ModalidadeId = modalidades.First(m => m.Nome == "Atletismo").Id },
                new Categoria { Nome = "Tênis - Duplas", Descricao = "Categoria para duplas no tênis", ModalidadeId = modalidades.First(m => m.Nome == "Tênis").Id },
                new Categoria { Nome = "Ciclismo - Estrada", Descricao = "Categoria para ciclismo em estrada", ModalidadeId = modalidades.First(m => m.Nome == "Ciclismo").Id }
                // Adicione mais categorias conforme necessário
            };

            // Adiciona as categorias ao contexto
            context.Categorias.AddRange(categorias);

            // Salva as alterações no banco de dados
            context.SaveChanges();
        }
        private static void InserirUsuarios(Contexto context)
        {
            // Verifica se já existem usuários
            if (context.Usuarios.Any())
            {
                return; // Sai se já existirem dados
            }

            // Lista de usuários a serem inseridos
            var usuarios = new List<Usuario>
            {
                new Usuario { Nome = "João", Sobrenome = "Silva", Email = "joao.silva@example.com", SenhaHash = Validators.GerarSenhaValida(), DataNascimento = new DateTime(1990, 1, 1), Cpf = Validators.GerarCpfValido(), Role = Role.Cliente },
                new Usuario { Nome = "Maria", Sobrenome = "Oliveira", Email = "maria.oliveira@example.com", SenhaHash = Validators.GerarSenhaValida(), DataNascimento = new DateTime(1995, 5, 15), Cpf = Validators.GerarCpfValido(), Role = Role.Cliente },
                new Usuario { Nome = "Carlos", Sobrenome = "Santos", Email = "carlos.santos@example.com", SenhaHash = Validators.GerarSenhaValida(), DataNascimento = new DateTime(1988, 8, 8), Cpf = Validators.GerarCpfValido(), Role = Role.Admin },
                new Usuario { Nome = "Ana", Sobrenome = "Costa", Email = "ana.costa@example.com", SenhaHash = Validators.GerarSenhaValida(), DataNascimento = new DateTime(2000, 12, 12), Cpf = Validators.GerarCpfValido(), Role = Role.Cliente },
                new Usuario { Nome = "Lucas", Sobrenome = "Almeida", Email = "lucas.almeida@example.com", SenhaHash = Validators.GerarSenhaValida(), DataNascimento = new DateTime(1992, 3, 20), Cpf = Validators.GerarCpfValido(), Role = Role.Cliente }
                // Adicione mais usuários conforme necessário
            };

            // Adiciona os usuários ao contexto
            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();
        }
        private static void InserirAtletasETimes(Contexto context)
        {
            // Verifica se já existem atletas ou times
            if (context.Atletas.Any() || context.Times.Any())
            {
                return; // Sai se já existirem dados
            }

            // Criação de uma lista de times
            var times = new List<Time>
            {
                new Time { Nome = "Equipe A", Municipio = "São Leo", UsuarioId = 1},
                new Time { Nome = "Equipe B", Municipio = "Esteio", UsuarioId = 2 },
                new Time { Nome = "Equipe C", Municipio = "Porto Alegre", UsuarioId = 3 },
                new Time { Nome = "Equipe D", Municipio = "Gravataí", UsuarioId = 4 },
                new Time { Nome = "Equipe E", Municipio = "Canoas", UsuarioId = 5 }
                // Adicione mais times conforme necessário
            };

            // Adiciona os times ao contexto
            context.Times.AddRange(times);
            context.SaveChanges(); // Salva para garantir que os IDs dos times sejam gerados

            // Criação de uma lista de atletas
            var atletas = new List<Atleta>
            {
                new Atleta { Nome = "João Silva", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Maria Oliveira", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Carlos Santos", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Ana Costa", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Lucas Almeida", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Renata Ferreira", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Fernando Lima", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Juliana Rocha", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Rafael Pereira", Cpf = Validators.GerarCpfValido() },
                new Atleta { Nome = "Isabela Martins", Cpf = Validators.GerarCpfValido() }
                // Adicione mais atletas conforme necessário
            };

            // Adiciona os atletas ao contexto
            context.Atletas.AddRange(atletas);
            context.SaveChanges(); // Salva para garantir que os IDs dos atletas sejam gerados

            // Associar atletas aos times (cada time terá 2 atletas)
            var atletasPorTime = atletas.GroupBy(a => a.Id % times.Count).ToList();
            
            foreach (var time in times)
            {
                // Seleciona atletas para o time baseado na divisão de índices
                var atletasDoTime = atletasPorTime.FirstOrDefault(g => g.Key == times.IndexOf(time));
                if (atletasDoTime != null)
                {
                    time.Atletas = atletasDoTime.ToList(); // Associa os atletas ao time
                }
            }

            // Atualiza os times com os atletas associados
            context.Times.UpdateRange(times);
            
            // Salva as alterações no banco de dados
            context.SaveChanges();
        }
        private static void InserirContasCorrentes(Contexto context)
        {
            // Verifica se já existem contas correntes
            if (context.ContasCorrentes.Any())
            {
                return; // Sai se já existirem dados
            }

            // Lista de contas correntes a serem inseridas
            var contasCorrentes = new List<ContaCorrente>
            {
                new ContaCorrente { Saldo = 1000.00m, UsuarioId = 1 }, // Exemplo: Usuário com Id = 1
                new ContaCorrente { Saldo = 1500.50m, UsuarioId = 2 }, // Exemplo: Usuário com Id = 2
                new ContaCorrente { Saldo = 2500.75m, UsuarioId = 3 }, // Exemplo: Usuário com Id = 3
                new ContaCorrente { Saldo = 500.00m, UsuarioId = 4 },   // Exemplo: Usuário com Id = 4
                new ContaCorrente { Saldo = 2500.00m, UsuarioId = 5 }      // Exemplo: Usuário com Id = 5
            };

            // Adiciona as contas correntes ao contexto
            context.ContasCorrentes.AddRange(contasCorrentes);
            context.SaveChanges();
        }
        private static void InserirPagamentos(Contexto context)
        {
            // Verifica se já existem pagamentos
            if (context.Pagamentos.Any())
            {
                return; // Sai se já existirem dados
            }

            // Lista de pagamentos a serem inseridos
            var pagamentos = new List<Pagamento>();

            // Exemplo de criação de múltiplos pagamentos com diferentes valores e motivos
            for (int i = 1; i <= 20; i++) // Cria 10 pagamentos
            {
                pagamentos.Add(new Pagamento
                {
                    Valor = 100.00m,
                    Moeda = "BRL",
                    DataRequisicao = DateTime.Now.AddDays(-i), // Data de requisição retrocedendo
                    DataRecebimento = DateTime.Now.AddDays(-i + 2), // Data de recebimento em 2 dias após a requisição
                    AprovadorId = 1, // Alterna entre dois administradores
                    Motivo = $"Pagamento de serviço {i}", // Motivo variado
                    Status = (i % 2 == 0) ? Status.Paga : Status.Pendente, // Alterna entre Paga e Pendente
                    TipoPagamento = (i % 3 == 0) ? TipoPagamento.CartaoDeCredito : TipoPagamento.Boleto, // Alterna tipos de pagamento
                    TokenPagSeguro = $"token{i:D6}" // Token formatado como "token000001", "token000002", etc.
                });
            }

            // Adiciona os pagamentos ao contexto
            context.Pagamentos.AddRange(pagamentos);
            context.SaveChanges();
        }
    }
}


