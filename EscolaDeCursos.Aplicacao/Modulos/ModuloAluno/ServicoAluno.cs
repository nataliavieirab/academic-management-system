using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public class ServicoAluno : ServicoBase<Aluno>
{
    private readonly IRepositorioAluno repositorioAluno;

    public ServicoAluno(
        IRepositorioAluno repositorioAluno
    )
    {
        this.repositorioAluno = repositorioAluno;
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
}