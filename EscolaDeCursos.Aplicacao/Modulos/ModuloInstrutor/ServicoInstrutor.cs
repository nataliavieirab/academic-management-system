using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public class ServicoInstrutor : ServicoBase<Instrutor>
{
    private readonly IRepositorioInstrutor repositorioInstrutor;
    private readonly IRepositorioTurma repositorioTurma;

    public ServicoInstrutor(
        IRepositorioInstrutor repositorioInstrutor,
        IRepositorioTurma repositorioTurma
    )
    {
        this.repositorioInstrutor = repositorioInstrutor;
        this.repositorioTurma = repositorioTurma;
    }

    public Result Cadastrar(CadastrarInstrutorDto dto)
    {
        if (ExisteInstrutorComMesmoCpf(dto.Cpf))
            return Falha(nameof(dto.Cpf), "Já existe um instrutor com este CPF cadastrado.");

        if (ExisteInstrutorComMesmoEmail(dto.Email))
            return Falha(nameof(dto.Email), "Já existe um instrutor com este E-mail cadastrado.");

        Instrutor novoInstrutor = new Instrutor(
            dto.Nome,
            dto.Cpf,
            dto.Telefone,
            dto.Email
        );

        Result resultadoValidacao = ValidarEntidade(novoInstrutor);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioInstrutor.Cadastrar(novoInstrutor);

        return Result.Ok();
    }

    public Result Editar(EditarInstrutorDto dto)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(dto.Id);

        if (instrutor == null)
            return Result.Fail("Instrutor não encontrado.");

        string cpfNormalizado = NormalizarCpf(dto.Cpf);
        string emailNormalizado = NormalizarEmail(dto.Email);

        if (ExisteInstrutorComMesmoCpf(cpfNormalizado, dto.Id))
            return Falha(nameof(dto.Cpf), "Já existe um instrutor com esse CPF.");

        if (ExisteInstrutorComMesmoEmail(emailNormalizado, dto.Id))
            return Falha(nameof(dto.Email), "Já existe um instrutor com esse e-mail.");

        Instrutor instrutorAtualizado = new Instrutor(
            dto.Nome,
            dto.Cpf,
            dto.Telefone,
            dto.Email
        );

        Result resultadoValidacao = ValidarEntidade(instrutorAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioInstrutor.Editar(dto.Id, instrutorAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return Result.Fail("Instrutor não encontrado.");

        if (PossuiTurmasVinculadas(id))
            return Falha(string.Empty, "Não é permitido excluir um instrutor vinculado a alguma turma.");

        repositorioInstrutor.Excluir(id);

        return Result.Ok();
    }

    private bool PossuiTurmasVinculadas(Guid instrutorId)
    {
        return repositorioTurma
            .SelecionarTodos()
            .Any(t => t.Instrutor.Id == instrutorId);
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
            .Any(i =>
                i.Id != idIgnorado &&
                i.Email == email
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
