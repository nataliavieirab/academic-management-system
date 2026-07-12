using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public class ServicoTurma : ServicoBase<Turma>
{
    private readonly IRepositorioTurma _repositorioTurma;
    private readonly IRepositorioInstrutor _repositorioInstrutor;

    public ServicoTurma(
        IRepositorioTurma repositorioTurma,
        IRepositorioInstrutor repositorioInstrutor
    )
    {
        _repositorioTurma = repositorioTurma;
        _repositorioInstrutor = repositorioInstrutor;
    }

    public Result Cadastrar(CadastrarTurmaDto dto)
    {
        Instrutor? instrutorSelecionado = null;

        if (dto.InstrutorId.HasValue)
        {
            instrutorSelecionado = _repositorioInstrutor.SelecionarPorId(dto.InstrutorId.Value);

            if (instrutorSelecionado is null)
                return Falha(nameof(dto.InstrutorId), "Selecione um instrutor válido.");
        }
        else
        {
            return Falha(nameof(dto.InstrutorId), "Selecione um instrutor válido.");
        }

        if (dto.DataInicio >= dto.DataTermino)
            return Falha(nameof(dto.DataInicio), "A data de início deve ser anterior à data de término.");

        Turma novaTurma = new Turma(
            dto.Nome,
            instrutorSelecionado,
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

    public List<ListarTurmasDto> SelecionarTodos()
    {
        return _repositorioTurma
            .SelecionarTodos()
            .Select(t => new ListarTurmasDto(
                t.Id,
                t.Nome,
                t.Instrutor?.Nome,
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
                turma.Instrutor?.Id,
                turma.Instrutor?.Nome,
                turma.CapacidadeMaxima,
                turma.DataInicio,
                turma.DataTermino
            )
        );
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