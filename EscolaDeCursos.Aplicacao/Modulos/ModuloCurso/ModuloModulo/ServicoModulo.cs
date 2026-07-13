using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;
using FluentResults;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso.ModuloModulo;

public class ServicoModulo : ServicoBase<Modulo>
{
    private readonly IRepositorioModulo repositorioModulo;
    private readonly IRepositorioCurso repositorioCurso;

    public ServicoModulo(
        IRepositorioModulo repositorioModulo,
        IRepositorioCurso repositorioCurso
    )
    {
        this.repositorioModulo = repositorioModulo;
        this.repositorioCurso = repositorioCurso;
    }

    public Result Cadastrar(CadastrarModuloDto dto)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(dto.CursoId);

        if (curso == null)
            return Falha(nameof(dto.CursoId), "Curso não encontrado.");

        if (ExisteModuloComMesmaOrdem(dto.CursoId, dto.Ordem))
            return Falha(nameof(dto.Ordem), "Já existe um módulo com esta ordem neste curso.");

        Modulo novoModulo = new Modulo(
            dto.Titulo,
            dto.Descricao,
            dto.Ordem,
            dto.Duracao,
            curso
        );

        Result resultadoValidacao = ValidarEntidade(novoModulo);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioModulo.Cadastrar(novoModulo);

        return Result.Ok();
    }

    public List<ListarModulosDto> SelecionarPorCurso(Guid cursoId)
    {
        return repositorioModulo
            .SelecionarTodos()
            .Where(m => m.CursoId == cursoId)
            .Select(m => new ListarModulosDto(
                m.Id,
                m.Titulo,
                m.Descricao,
                m.Ordem,
                m.Duracao,
                m.CursoId,
                m.Curso.Titulo
            ))
            .ToList();
    }

    private bool ExisteModuloComMesmaOrdem(Guid cursoId, int ordem, Guid? idIgnorado = null)
    {
        return repositorioModulo
            .SelecionarTodos()
            .Any(m =>
                m.Id != idIgnorado &&
                m.CursoId == cursoId &&
                m.Ordem == ordem
            );
    }
}
