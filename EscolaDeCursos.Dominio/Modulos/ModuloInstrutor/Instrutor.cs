using System.Net.Mail;
using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;


public class Instrutor : EntidadeBase<Instrutor>
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Instrutor()
    {
    }

    public Instrutor(string nome, string cpf, string telefone, string email)
    {
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Email = email;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        ValidarNome(erros);
        ValidarCpf(erros);
        ValidarTelefone(erros);
        ValidarEmail(erros);

        return erros;
    }

    private void ValidarNome(List<string> erros)
    {
        if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");
    }

    private void ValidarEmail(List<string> erros)
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            erros.Add("O campo \"Email\" é obrigatório.");
            return;
        }

        try
        {
            MailAddress endereco = new MailAddress(Email);

            if (endereco.Address != Email)
                erros.Add("O campo \"Email\" possui formato inválido.");
        }
        catch
        {
            erros.Add("O campo \"Email\" possui formato inválido.");
        }
    }

    private void ValidarTelefone(List<string> erros)
    {
        string telefoneEncurtado = RemoverFormatacao(Telefone);

        if (telefoneEncurtado.StartsWith("0"))
            telefoneEncurtado = telefoneEncurtado.Substring(1);

        bool telefoneValido = true;

        if (telefoneEncurtado.Length < 10 || telefoneEncurtado.Length > 11)
        {
            erros.Add("O campo \"Telefone\" deve conter entre 10 e 11 dígitos.");
            telefoneValido = false;
        }

        if (!ContemSomenteDigitos(telefoneEncurtado))
        {
            erros.Add("O campo \"Telefone\" deve conter apenas dígitos.");
            telefoneValido = false;
        }

        if (telefoneValido)
        {
            if (telefoneEncurtado.Length == 10)
            {
                Telefone = Convert.ToUInt64(telefoneEncurtado)
                    .ToString(@"\(00\) 0000\-0000");
            }
            else
            {
                Telefone = Convert.ToUInt64(telefoneEncurtado)
                    .ToString(@"\(00\) 00000\-0000");
            }
        }
    }

    private void ValidarCpf(List<string> erros)
    {
        if (string.IsNullOrWhiteSpace(Cpf))
        {
            erros.Add("O campo \"Cpf\" deve ser preenchido.");
            return;
        }

        string cpfEncurtado = RemoverFormatacao(Cpf);

        bool cpfValido = true;

        if (cpfEncurtado.Length != 11)
        {
            erros.Add("O campo \"Cpf\" deve conter 11 dígitos.");
            cpfValido = false;
        }

        if (!ContemSomenteDigitos(cpfEncurtado))
        {
            erros.Add("O campo \"Cpf\" deve conter somente dígitos.");
            cpfValido = false;
        }

        if (cpfValido)
        {
            Cpf = Convert.ToUInt64(cpfEncurtado)
                .ToString(@"000\.000\.000\-00");
        }
    }

    public override void Atualizar(Instrutor entidadeAtualizada)
    {
        Instrutor instrutorAtualizado = entidadeAtualizada;

        Nome = instrutorAtualizado.Nome;
        Cpf = instrutorAtualizado.Cpf;
        Telefone = instrutorAtualizado.Telefone;
        Email = instrutorAtualizado.Email;
    }

    private bool ContemSomenteDigitos(string valor)
    {
        for (int i = 0; i < valor.Length; i++)
        {
            if (!char.IsDigit(valor[i]))
                return false;
        }

        return true;
    }

    public static string RemoverFormatacao(string valor)
    {
        return valor
            .Replace(" ", "")
            .Replace("-", "")
            .Replace(".", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "");
    }

}