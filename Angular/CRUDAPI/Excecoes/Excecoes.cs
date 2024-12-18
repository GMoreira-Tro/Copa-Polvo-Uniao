using System;

public class EmailJaCadastradoException : Exception
{
    public EmailJaCadastradoException() : base("Email já cadastrado.") { }
}

public class CpfJaCadastradoException : Exception
{
    public CpfJaCadastradoException() : base("CPF já cadastrado.") { }
}

public class CpfInvalidoException : Exception
{
    public CpfInvalidoException() : base("CPF inválido.") { }
}

public class CampoObrigatorioException : Exception
{
    public CampoObrigatorioException(string campo) : base($"O campo '{campo}' é obrigatório.") { }
}

public class SaldoNegativoException : Exception
{
    public SaldoNegativoException() : base($"O saldo não pode ser negativo.") { }
}

public class StatusInscricaoInvalidoException : Exception
{
    public StatusInscricaoInvalidoException() 
        : base("O status da inscrição deve ser 'pendente', 'paga', 'aceita' ou 'recusada'.") { }
}

public class EstadoNaoPertenceAoPaisException : Exception
{
    public EstadoNaoPertenceAoPaisException() : base("Estado não pertence ao país.") { }
}

public class CidadeNaoPertenceAoEstadoException : Exception
{
    public CidadeNaoPertenceAoEstadoException() : base("Cidade não pertence ao estado.") { }
}

public class SenhaDeveConterNoMinimo8CaracteresException : Exception
{
    public SenhaDeveConterNoMinimo8CaracteresException() : base("A senha deve conter no mínimo 8 caracteres.") { }
}

public class SenhaDeveConterRegexMatchException : Exception
{
    public SenhaDeveConterRegexMatchException() : base("A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.") { }
}

public class EmailJaCadastradoEntreCompetidoresException : Exception
{
    public EmailJaCadastradoEntreCompetidoresException() : base("Você já cadastrou um competidor com este email.") { }
}

public class EmailInvalidoException : Exception
{
    public EmailInvalidoException() : base("O email fornecido não é válido.") { }
}

public class HistoricoFinanceiroJaPossuiUsuarioException : Exception
{
    public HistoricoFinanceiroJaPossuiUsuarioException() : base("Já existe um histórico financeiro para o usuário fornecido.") { }
}
