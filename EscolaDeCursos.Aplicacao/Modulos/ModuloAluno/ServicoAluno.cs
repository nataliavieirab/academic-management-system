using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public class ServicoAluno : ServicoBase<Aluno>
{
    private readonly IRepositorioAluno repositorioAluno;
    private readonly IRepositorioMatricula repositorioMatricula;

    public ServicoAluno(
        IRepositorioAluno repositorioAluno,
        IRepositorioMatricula repositorioMatricula
    )
    {
        this.repositorioAluno = repositorioAluno;
        this.repositorioMatricula = repositorioMatricula;
    }

    public Result Cadastrar(CadastrarAlunoDto dto)
    {
        if (ExisteAlunoComMesmoCpf(dto.Cpf))
            return Falha(nameof(dto.Cpf), "Já existe um aluno com este CPF cadastrado.");

        if (ExisteAlunoComMesmoEmail(dto.Email))
            return Falha(nameof(dto.Email), "Já existe um aluno com este E-mail cadastrado.");

        Aluno novoAluno = new Aluno(
            dto.Nome,
            dto.Cpf,
            dto.Telefone,
            dto.Email,
            dto.Endereco
        );

        Result resultadoValidacao = ValidarEntidade(novoAluno);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioAluno.Cadastrar(novoAluno);

        return Result.Ok();
    }

    public Result Editar(EditarAlunoDto dto)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(dto.Id);

        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        string cpfNormalizado = NormalizarCpf(dto.Cpf);
        string emailNormalizado = NormalizarEmail(dto.Email);

        if (ExisteAlunoComMesmoCpf(cpfNormalizado, dto.Id))
            return Falha(nameof(dto.Cpf), "Já existe um aluno com esse CPF.");

        if (ExisteAlunoComMesmoEmail(emailNormalizado, dto.Id))
            return Falha(nameof(dto.Email), "Já existe um aluno com esse e-mail.");

        Aluno alunoAtualizado = new Aluno(
            dto.Nome,
            cpfNormalizado,
            dto.Telefone,
            emailNormalizado,
            dto.Endereco
        );

        Result resultadoValidacao = ValidarEntidade(alunoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioAluno.Editar(dto.Id, alunoAtualizado);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        List<Matricula> matriculasDoAluno = repositorioMatricula
            .SelecionarTodos()
            .Where(m => m.AlunoId == id)
            .ToList();

        if (matriculasDoAluno.Any(m => m.Situacao == SituacaoAluno.Ativa))
            return Falha(
                string.Empty,
                "Não é permitido excluir um aluno com matrícula ativa. Cancele ou conclua a matrícula antes de excluir."
            );

        foreach (Matricula matricula in matriculasDoAluno)
            repositorioMatricula.Excluir(matricula.Id);

        repositorioAluno.Excluir(id);

        return Result.Ok();
    }

    private string NormalizarCpf(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    private bool ExisteAlunoComMesmoCpf(string cpf, Guid? idIgnorado = null)
    {
        string cpfNormalizado = NormalizarCpf(cpf);

        return repositorioAluno
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

    private bool ExisteAlunoComMesmoEmail(string email, Guid? idIgnorado = null)
    {
        return repositorioAluno
            .SelecionarTodos()
            .Any(a =>
                a.Id != idIgnorado &&
                a.Email == email
            );
    }

    public List<ListarAlunosDto> SelecionarTodos()
    {
        return repositorioAluno
            .SelecionarTodos()
            .Select(a => new ListarAlunosDto(
                a.Id,
                a.Nome,
                a.Cpf,
                a.Telefone,
                a.Email,
                a.Endereco
            ))
            .ToList();
    }

    public Result<DetalhesAlunoDto> SelecionarPorId(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return Result.Fail("Aluno não encontrado.");

        return Result.Ok(
            new DetalhesAlunoDto(
                aluno.Id,
                aluno.Nome,
                aluno.Cpf,
                aluno.Telefone,
                aluno.Email,
                aluno.Endereco
            )
        );
    }
}