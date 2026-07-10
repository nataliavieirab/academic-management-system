using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public class ServicoInstrutor : ServicoBase<Instrutor>
{
    private readonly IRepositorioInstrutor repositorioInstrutor;

    public ServicoInstrutor(
        IRepositorioInstrutor repositorioInstrutor
    )
    {
        this.repositorioInstrutor = repositorioInstrutor;
    }

    private string NormalizarCpf(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    private bool ExisteInstrutorComMesmoCpf(string cpf, Guid? idIgnorado = null)
    {
        string cpfNormalizado = NormalizarCpf(cpf);

        return repositorioInstrutor
            .SelecionarTodos()
            .Any(a =>
                a.Id != idIgnorado &&
                NormalizarCpf(a.Cpf) == cpfNormalizado
            );
    }

    private string NormalizarEmail(string email)
    {
        return email.Trim().ToLower();
    }

    private bool ExisteInstrutorComMesmoEmail(string email, Guid? idIgnorado = null)
    {
        return repositorioInstrutor
            .SelecionarTodos()
            .Any(a =>
                a.Id != idIgnorado &&
                a.Email == email
            );
    }

    public List<ListarInstrutoresDto> SelecionarTodos()
    {
        return repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new ListarInstrutoresDto(
                i.Id,
                i.Nome,
                i.Cpf,
                i.Telefone,
                i.Email
            ))
            .ToList();
    }

    public Result<DetalhesInstrutorDto> SelecionarPorId(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return Result.Fail("Instrutor não encontrado.");

        return Result.Ok(
            new DetalhesInstrutorDto(
                instrutor.Id,
                instrutor.Nome,
                instrutor.Cpf,
                instrutor.Telefone,
                instrutor.Email
            )
        );
    }
}
