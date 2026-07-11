using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma repositorioTurma;

    public ServicoTurma(
        IRepositorioTurma repositorioTurma
    )
    {
        this.repositorioTurma = repositorioTurma;
    }

    // public Result Cadastrar(CadastrarTurmaDto dto)
    // {
    //     Turma novaTurma = new Turma(
    //         dto.Nome,
    //         dto.CursoNome,          
    //         dto.InstrutorNome,      
    //         dto.CapacidadeMaxima,
    //         dto.DataInicio,
    //         dto.DataTermino
    //     );

    //     Result resultadoValidacao = ValidarEntidade(novaTurma);

    //     if (resultadoValidacao.IsFailed)
    //         return resultadoValidacao;

    //     repositorioTurma.Cadastrar(novaTurma);

    //     return Result.Ok();
    // }


    // public Result Editar(EditarTurmaDto dto)
    // {
    //     Turma? turma = repositorioTurma.SelecionarPorId(dto.Id);

    //     if (turma == null)
    //         return Result.Fail("Turma não encontrada.");

    //     Turma turmaAtualizada = new Turma(
    //         dto.Nome,
    //         dto.Curso,
    //         dto.Instrutor,
    //         dto.CapacidadeMaxima,
    //         dto.DataInicio,
    //         dto.DataTermino
    //     );

    //     Result resultadoValidacao = ValidarEntidade(turmaAtualizada);

    //     if (resultadoValidacao.IsFailed)
    //         return resultadoValidacao;

    //     repositorioTurma.Editar(dto.Id, turmaAtualizada);

    //     return Result.Ok();
    // }

    // public Result Excluir(Guid id)
    // {
    //     Turma? turma = repositorioTurma.SelecionarPorId(id);

    //     if (turma == null)
    //         return Result.Fail("Turma não encontrada.");

    //     if (turma.Alunos.Any())
    //         return Result.Fail("Não é permitido excluir turmas com alunos matriculados.");

    //     repositorioTurma.Excluir(id);

    //     return Result.Ok();
    // }

    public List<ListarTurmasDto> SelecionarTodos()
    {
        return repositorioTurma
            .SelecionarTodos()
            .Select(t => new ListarTurmasDto(
                t.Id,
                t.Nome,
                // t.Curso.Nome,
                t.Instrutor.Nome,
                t.CapacidadeMaxima,
                t.DataInicio,
                t.DataTermino
            ))
            .ToList();
    }

    public Result<DetalhesTurmaDto> SelecionarPorId(Guid id)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(id);

        if (turma == null)
            return Result.Fail("Turma não encontrada.");

        return Result.Ok(
            new DetalhesTurmaDto(
                turma.Id,
                turma.Nome,
                // turma.Curso.Nome,
                turma.Instrutor.Nome,
                turma.CapacidadeMaxima,
                turma.DataInicio,
                turma.DataTermino
            )
        );
    }

}