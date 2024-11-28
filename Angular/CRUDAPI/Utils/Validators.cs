using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

public static partial class Validators
{
    // Propriedade estática para armazenar a expressão regular
    private static readonly Regex senhaRegex = SenhaRegex();
    public static bool IsValidEmail(string email)
    {
        // Expressão regular para validar email
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        // Verifica se o email corresponde ao padrão da expressão regular
        return Regex.IsMatch(email, pattern);
    }

    public static bool ValidarSenha(string senha)
    {
        // Verificar o comprimento mínimo da senha
        if (senha.Length < 8)
        {
            throw new SenhaDeveConterNoMinimo8CaracteresException();
        }

        // Verificar se a senha contém letras maiúsculas, minúsculas, números e caracteres especiais
        if (!senhaRegex.IsMatch(senha))
        {
            throw new SenhaDeveConterRegexMatchException();
        }

        return true;
    }

    public static bool ValidarRG(string rg)
    {
        // Verifica se contém apenas números
        if (!rg.All(char.IsDigit))
            return false;

        // O RG deve ter exatamente 9 dígitos
        return rg.Length == 9;
    }

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%&*!?\-_()])[A-Za-z\d@#$%&*!?\-_()]+$")]
    private static partial Regex SenhaRegex();

    public static bool ValidarCPF(string documento)
    {
        CPFCNPJ.Main main = new();
        return main.IsValidCPFCNPJ(documento);
    }

    public static string GerarCpfValido()
    {
        Random random = new Random();
        int[] cpf = new int[9];

        // Gera os 9 primeiros dígitos
        for (int i = 0; i < 9; i++)
        {
            cpf[i] = random.Next(0, 10);
        }

        // Calcula o primeiro dígito verificador
        int primeiroDigito = CalcularDigitoVerificador(cpf, 10);
        cpf = cpf.Append(primeiroDigito).ToArray();

        // Calcula o segundo dígito verificador
        int segundoDigito = CalcularDigitoVerificador(cpf, 11);
        cpf = cpf.Append(segundoDigito).ToArray();

        // Retorna o CPF como string formatada
        return string.Join("", cpf);
    }

    private static int CalcularDigitoVerificador(int[] cpf, int peso)
    {
        int soma = 0;
        for (int i = 0; i < cpf.Length; i++)
        {
            soma += cpf[i] * peso;
            peso--;
        }
        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }

    public static string GerarSenhaValida()
    {
        const string letrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string letrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        const string numeros = "0123456789";
        const string simbolos = "!@#$%^&*()-_=+[]{};:,.<>?";

        var random = new Random();
        var senha = new List<char>
        {
            letrasMaiusculas[random.Next(letrasMaiusculas.Length)],
            letrasMinusculas[random.Next(letrasMinusculas.Length)],
            numeros[random.Next(numeros.Length)],
            simbolos[random.Next(simbolos.Length)]
        };

        // Preencher o restante da senha até 8 caracteres
        for (int i = senha.Count; i < 8; i++)
        {
            senha.Add(letrasMaiusculas[random.Next(letrasMaiusculas.Length)]);
        }

        // Embaralhar a senha
        return new string(senha.OrderBy(c => random.Next()).ToArray());
    }

    public static string GerarTokenUnico(string email)
    {
        using (var sha256 = SHA256.Create())
        {
            // Combina o e-mail com a data/hora atual para gerar um hash único
            string data = email + DateTime.UtcNow.Ticks;
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hashBytes)
                        .Replace("+", "") // Remove caracteres problemáticos em URLs
                        .Replace("/", "")
                        .Replace("=", "");
        }
    }
}
