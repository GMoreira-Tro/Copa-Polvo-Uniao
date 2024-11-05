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
            InserirPagamentosEPagamentosContasCorrente(context);
            InserirInscricoes(context);
            InserirConfrontosEConfrontoInscricoes(context);
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
        private static void InserirPagamentosEPagamentosContasCorrente(Contexto context)
        {
            // Verifica se já existem registros na tabela PagamentoContaCorrente
            if (context.PagamentoContaCorrentes.Any())
            {
                return; // Sai se já existirem dados
            }

            // Obtém a conta corrente do administrador
            var contaAdmin = context.ContasCorrentes
                                    .Where(c => c.Usuario != null && c.Usuario.Role == Role.Admin)
                                    .FirstOrDefault();

            // Verifica se a conta do administrador existe
            if (contaAdmin == null)
            {
                throw new Exception("Não há conta corrente associada ao administrador.");
            }

            // Obtém as contas correntes dos usuários clientes
            var contasClientes = context.ContasCorrentes
                                        .Where(c => c.Usuario != null && c.Usuario.Role == Role.Cliente)
                                        .ToList();

            // Cria uma lista para armazenar os novos pagamentos em contas correntes
            var pagamentosContasCorrente = new List<PagamentoContaCorrente>();

            // Itera sobre cada conta de cliente para gerar pagamentos para o administrador
            foreach (var contaCliente in contasClientes)
            {
                // Cria um novo pagamento para cada cliente
                var pagamento = new Pagamento
                {
                    Valor = 500.00m, // Valor do pagamento, altere conforme necessário
                    Moeda = "BRL",
                    DataRequisicao = DateTime.Now,
                    Status = Status.Pendente,
                    TipoPagamento = TipoPagamento.CartaoDeCredito, // ou outro tipo conforme desejado
                    AprovadorId = contaAdmin.UsuarioId, // ID do admin que aprovará
                    TokenPagSeguro = "TOKEN_EXEMPLO" // Token de exemplo, altere se necessário
                };

                // Adiciona o pagamento ao contexto
                context.Pagamentos.Add(pagamento);
                context.SaveChanges(); // Salva o pagamento para obter o Id gerado

                // Adiciona a transação do cliente para o administrador
                pagamentosContasCorrente.Add(new PagamentoContaCorrente
                {
                    PagamentoId = pagamento.Id,
                    ContaCorrenteId = contaCliente.Id, // Conta do cliente como pagador
                    ContaCorrenteSolicitante = true, // Pagador (cliente)
                    Observacao = $"Pagamento de Cliente (Conta {contaCliente.Id}) para Admin (Conta {contaAdmin.Id})"
                });

                // Adiciona a transação do administrador como recebedor
                pagamentosContasCorrente.Add(new PagamentoContaCorrente
                {
                    PagamentoId = pagamento.Id,
                    ContaCorrenteId = contaAdmin.Id, // Conta do administrador como recebedor
                    ContaCorrenteSolicitante = false, // Recebedor (admin)
                    Observacao = $"Recebimento pelo Admin (Conta {contaAdmin.Id}) de Cliente (Conta {contaCliente.Id})"
                });
            }

            // Adiciona as transações ao contexto e salva
            context.PagamentoContaCorrentes.AddRange(pagamentosContasCorrente);
            context.SaveChanges();
        }
        private static void InserirInscricoes(Contexto context)
        {
            // Verifica se já existem inscrições
            if (context.Inscricoes.Any())
            {
                return; // Sai se já existirem dados
            }

            // Obtém a primeira categoria da lista
            var primeiraCategoria = context.Categorias.FirstOrDefault();
            if (primeiraCategoria == null)
            {
                Console.WriteLine("Nenhuma categoria encontrada!");
                return;
            }

            // Obtém todos os times existentes
            var times = context.Times.ToList();

            // Cria uma lista para armazenar as inscrições
            var inscricoes = new List<Inscricao>();

            // Para cada time, realiza uma inscrição na primeira categoria encontrada
            foreach (var time in times)
            {
                // Encontra a ContaCorrente do usuário dono do time
                var contaCorrente = context.ContasCorrentes
                    .FirstOrDefault(cc => cc.UsuarioId == time.UsuarioId);
                var pagamentoContaCorrente = context.PagamentoContaCorrentes.FirstOrDefault(pcc => contaCorrente != null && 
                                                                                                pcc.ContaCorrenteId == contaCorrente.Id);
                var inscricao = new Inscricao
                {
                    CategoriaId = primeiraCategoria.Id,       // Categoria escolhida
                    TimeId = time.Id,                         // Time a ser inscrito
                    PagamentoContaCorrenteId = pagamentoContaCorrente != null ? pagamentoContaCorrente.Id : 0,
                    Posição = null,                           // Posição ainda não determinada
                    WO = false,                               // Indica que não houve WO
                    PremioResgatavelId = null  
                };

                // Adiciona a inscrição à lista
                inscricoes.Add(inscricao);
            }

            // Adiciona as inscrições ao contexto e salva no banco de dados
            context.Inscricoes.AddRange(inscricoes);
            context.SaveChanges();
        }
        private static void InserirConfrontosEConfrontoInscricoes(Contexto context)
        {
            // Verifica se já existem confrontos
            if (context.Confrontos.Any())
            {
                return; // Sai se já existirem dados
            }

            // Obtém todas as inscrições existentes
            var inscricoes = context.Inscricoes.Include(i => i.Categoria).ToList(); // Inclui a categoria da inscrição
            if (!inscricoes.Any())
            {
                Console.WriteLine("Nenhuma inscrição encontrada!");
                return;
            }

            // Cria uma lista para armazenar os confrontos
            var confrontos = new List<Confronto>();
            var confrontoInscricoes = new List<ConfrontoInscricao>();

            // Agrupar as inscrições por categoria
            var inscricoesPorCategoria = inscricoes.GroupBy(i => i.CategoriaId);

            foreach (var grupo in inscricoesPorCategoria)
            {
                var listaInscricoes = grupo.ToList();
                
                // Agrupar as inscrições para formar confrontos
                for (int i = 0; i < listaInscricoes.Count; i += 2)
                {
                    // Verifica se há um par para formar um confronto
                    if (i + 1 < listaInscricoes.Count)
                    {
                        var confronto = new Confronto
                        {
                            DataInicio = DateTime.Now, // Define a data de início como agora
                            DataTermino = DateTime.Now.AddHours(1), // Define a data de término como uma hora após o início
                            Local = "Local Padrão", // Você pode personalizar o local ou torná-lo dinâmico
                        };

                        // Adiciona o confronto à lista
                        confrontos.Add(confronto);

                        // Cria as inscrições de confronto para cada time
                        confrontoInscricoes.Add(new ConfrontoInscricao { Confronto = confronto, InscricaoId = listaInscricoes[i].Id });
                        confrontoInscricoes.Add(new ConfrontoInscricao { Confronto = confronto, InscricaoId = listaInscricoes[i + 1].Id });
                    }
                }
            }

            // Adiciona os confrontos e confrontos inscrições ao contexto e salva no banco de dados
            context.Confrontos.AddRange(confrontos);
            context.ConfrontoInscricoes.AddRange(confrontoInscricoes);
            context.SaveChanges();
        }
    }
}


