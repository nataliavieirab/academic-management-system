using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma _repositorioTurma;
    private readonly IRepositorioInstrutor _repositorioInstrutor;
    private readonly IRepositorioMatricula _repositorioMatricula;
    private readonly IRepositorioCurso _repositorioCurso;

    public ServicoTurma(
        IRepositorioTurma repositorioTurma,
        IRepositorioInstrutor repositorioInstrutor,
        IRepositorioMatricula repositorioMatricula,
        IRepositorioCurso repositorioCurso
    )
    {
        _repositorioTurma = repositorioTurma;
        _repositorioInstrutor = repositorioInstrutor;
        _repositorioMatricula = repositorioMatricula;
        _repositorioCurso = repositorioCurso;
    }

    public Result Cadastrar(CadastrarTurmaDto dto)
    {
        Instrutor? instrutor = _repositorioInstrutor.SelecionarPorId(dto.InstrutorId);

        if (instrutor == null)
            return Falha(nameof(dto.InstrutorId), "Instrutor não encontrado.");


        Curso? curso = _repositorioCurso.SelecionarPorId(dto.CursoId);

        if (curso == null)
            return Falha(nameof(dto.CursoId), "Curso não encontrado.");

        if (dto.DataInicio >= dto.DataTermino)
            return Falha(nameof(dto.DataInicio), "A data de início deve ser anterior à data de término.");

        Turma novaTurma = new Turma(
            dto.Nome,
            curso,
            instrutor,
            dto.CapacidadeMaxima,
            dto.DataInicio,
            dto.DataTermino
        );

        Result resultadoValidacao = ValidarEntidade(novaTurma);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        _repositorioTurma.Cadastrar(novaTurma);

        return Result.Ok();
    }

    public Result Editar(EditarTurmaDto dto)
    {
        Turma? turma = _repositorioTurma.SelecionarPorId(dto.Id);

        if (turma is null)
            return Falha(nameof(dto.Id), "Turma não encontrada.");

        Instrutor? instrutor = _repositorioInstrutor.SelecionarPorId(dto.InstrutorId);

        if (instrutor == null)
            return Falha(nameof(dto.InstrutorId), "Instrutor não encontrado.");


        Curso? curso = _repositorioCurso.SelecionarPorId(dto.CursoId);

        if (curso == null)
            return Falha(nameof(dto.CursoId), "Curso não encontrado.");

        if (dto.DataInicio >= dto.DataTermino)
            return Falha(nameof(dto.DataInicio), "A data de início deve ser anterior à data de término.");

        Turma turmaAtualizada = new Turma(
            dto.Nome,
            curso,
            instrutor,
            dto.CapacidadeMaxima,
            dto.DataInicio,
            dto.DataTermino
        );

        Result resultadoValidacao = ValidarEntidade(turmaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        _repositorioTurma.Editar(dto.Id, turmaAtualizada);

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Turma? turma = _repositorioTurma.SelecionarPorId(id);

        if (turma is null)
            return Falha(nameof(id), "Turma não encontrada.");

        bool possuiMatriculasAtivas = _repositorioMatricula
            .SelecionarTodos()
            .Any(m => m.TurmaId == id && m.Situacao == SituacaoAluno.Ativa);

        if (possuiMatriculasAtivas)
            return Falha(nameof(id), "Não é permitido excluir turmas com alunos matriculados.");

        _repositorioTurma.Excluir(id);

        return Result.Ok();
    }

    public List<ListarTurmasDto> SelecionarTodos()
    {
        return _repositorioTurma
            .SelecionarTodos()
            .Select(t => new ListarTurmasDto(
                t.Id,
                t.Nome,
                t.Curso.Titulo,
                t.Instrutor.Nome,
                t.CapacidadeMaxima,
                t.DataInicio,
                t.DataTermino
            ))
            .ToList();
    }

    public Result<DetalhesTurmaDto> SelecionarPorId(Guid id)
    {
        Turma? turma = _repositorioTurma.SelecionarPorId(id);

        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok(
            new DetalhesTurmaDto(
                turma.Id,
                turma.Nome,
                turma.Curso.Id,
                turma.Curso.Titulo,
                turma.Instrutor.Id,
                turma.Instrutor.Nome,
                turma.CapacidadeMaxima,
                turma.DataInicio,
                turma.DataTermino
            )
        );
    }

    public List<OpcaoCursoDto> SelecionarCurso()
    {
        return _repositorioCurso
            .SelecionarTodos()
            .Select(c => new OpcaoCursoDto(c.Id, c.Titulo))
            .ToList();
    }

    public List<OpcaoInstrutorDto> SelecionarInstrutor()
    {
        return _repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new OpcaoInstrutorDto(i.Id, i.Nome))
            .ToList();
    }

    private new static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}
