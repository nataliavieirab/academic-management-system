using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using FluentResults;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public class ServicoMatricula : ServicoBase<Matricula>
{
    private readonly IRepositorioMatricula _repositorioMatricula;
    private readonly IRepositorioAluno _repositorioAluno;
    private readonly IRepositorioTurma _repositorioTurma;

    public ServicoMatricula(
        IRepositorioMatricula repositorioMatricula,
        IRepositorioAluno repositorioAluno,
        IRepositorioTurma repositorioTurma)
    {
        _repositorioMatricula = repositorioMatricula;
        _repositorioAluno = repositorioAluno;
        _repositorioTurma = repositorioTurma;
    }

    public Result Cadastrar(CadastrarMatriculaDto dto)
    {
        Aluno? aluno = _repositorioAluno.SelecionarPorId(dto.AlunoId);

        if (aluno is null)
            return Falha(nameof(dto.AlunoId), "Selecione um aluno válido.");

        Turma? turma = _repositorioTurma.SelecionarPorId(dto.TurmaId);

        if (turma is null)
            return Falha(nameof(dto.TurmaId), "Turma não encontrada.");

        if (ExisteMatricula(dto.AlunoId, dto.TurmaId))
            return Falha(
                nameof(dto.AlunoId),
                "Este aluno já está matriculado nesta turma. Para alterar a situação da matrícula, edite o registro existente."
            );

        if (ContarMatriculasAtivas(dto.TurmaId) >= turma.CapacidadeMaxima)
            return Falha(nameof(dto.TurmaId), "Não é possível cadastrar um novo aluno nesta turma, pois a capacidade máxima foi atingida.");

        Matricula novaMatricula = new Matricula(
            aluno,
            turma,
            DateOnly.FromDateTime(DateTime.Now),
            SituacaoAluno.Ativa
        );

        Result resultadoValidacao = ValidarEntidade(novaMatricula);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        _repositorioMatricula.Cadastrar(novaMatricula);

        return Result.Ok();
    }

    public Result Editar(EditarMatriculaDto dto)
    {
        Matricula? matricula = _repositorioMatricula.SelecionarPorId(dto.Id);

        if (matricula is null)
            return Falha(nameof(dto.Id), "Matrícula não encontrada.");

        if (!Enum.IsDefined(dto.Situacao))
            return Falha(nameof(dto.Situacao), "O campo \"Situação\" é inválido.");

        if (dto.Situacao == SituacaoAluno.Ativa && matricula.Situacao != SituacaoAluno.Ativa)
        {
            Turma? turma = _repositorioTurma.SelecionarPorId(matricula.TurmaId);

            if (turma is null)
                return Falha(nameof(dto.Id), "Turma não encontrada.");

            if (ContarMatriculasAtivas(matricula.TurmaId) >= turma.CapacidadeMaxima)
                return Falha(nameof(dto.Situacao), "Não é possível reativar esta matrícula, pois a capacidade máxima da turma foi atingida.");
        }

        Matricula matriculaAtualizada = new Matricula(
            matricula.Aluno!,
            matricula.Turma!,
            matricula.Data,
            dto.Situacao
        );

        Result resultadoValidacao = ValidarEntidade(matriculaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        _repositorioMatricula.Editar(dto.Id, matriculaAtualizada);

        return Result.Ok();
    }

    public List<ListarMatriculasDto> SelecionarPorTurma(Guid turmaId, SituacaoAluno? situacao = null)
    {
        return _repositorioMatricula
            .Selecionar(turmaId, situacao)
            .Select(m => new ListarMatriculasDto(
                m.Id,
                m.TurmaId,
                m.Aluno?.Nome ?? string.Empty,
                m.Data,
                m.Situacao
            ))
            .ToList();
    }

    public Result<DetalhesMatriculaDto> SelecionarPorId(Guid id)
    {
        Matricula? matricula = _repositorioMatricula.SelecionarPorId(id);

        if (matricula is null)
            return Result.Fail("Matrícula não encontrada.");

        return Result.Ok(
            new DetalhesMatriculaDto(
                matricula.Id,
                matricula.AlunoId,
                matricula.Aluno?.Nome ?? string.Empty,
                matricula.TurmaId,
                matricula.Turma?.Nome ?? string.Empty,
                matricula.Data,
                matricula.Situacao
            )
        );
    }

    public List<OpcaoAlunoDto> SelecionarAlunos()
    {
        return _repositorioAluno
            .SelecionarTodos()
            .Select(a => new OpcaoAlunoDto(a.Id, a.Nome))
            .ToList();
    }

    private bool ExisteMatricula(Guid alunoId, Guid turmaId)
    {
        return _repositorioMatricula
            .SelecionarTodos()
            .Any(m =>
                m.AlunoId == alunoId &&
                m.TurmaId == turmaId
            );
    }

    private int ContarMatriculasAtivas(Guid turmaId)
    {
        return _repositorioMatricula
            .SelecionarTodos()
            .Count(m =>
                m.TurmaId == turmaId &&
                m.Situacao == SituacaoAluno.Ativa
            );
    }
}
