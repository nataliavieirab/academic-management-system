using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public class ServicoCurso : ServicoBase<Curso>
{
    private readonly IRepositorioCurso repositorioCurso;
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoCurso(
        IRepositorioCurso repositorioCurso,
        IRepositorioCategoria repositorioCategoria
    )
    {
        this.repositorioCurso = repositorioCurso;
        this.repositorioCategoria = repositorioCategoria;
    }

    public Result Cadastrar(CadastrarCursoDto dto)
    {
        if (ExisteCursoComMesmoTitulo(dto.Titulo))
            return Falha(nameof(dto.Titulo), "Já existe um curso com este título.");

        Categoria? categoria = repositorioCategoria.SelecionarPorId(dto.CategoriaId);

        if (categoria == null)
            return Falha(nameof(dto.CategoriaId), "Categoria não encontrada.");

        Curso novoCurso = new Curso(
            dto.Titulo,
            dto.Descricao,
            categoria,
            dto.Nivel,
            dto.CargaHoraria
        );

        Result resultadoValidacao = ValidarEntidade(novoCurso);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCurso.Cadastrar(novoCurso);

        return Result.Ok();
    }

    public List<ListarCursosDto> SelecionarTodos()
    {
        return repositorioCurso
            .SelecionarTodos()
            .Select(c => new ListarCursosDto(
                c.Id,
                c.Titulo,
                c.CategoriaId,
                c.Categoria.Titulo,
                c.Nivel,
                c.CargaHoraria
            ))
            .ToList();
    }

    private bool ExisteCursoComMesmoTitulo(string titulo, Guid? idIgnorado = null)
    {
        string tituloNormalizado = NormalizarTitulo(titulo);

        return repositorioCurso
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarTitulo(c.Titulo) == tituloNormalizado
            );
    }

    private static string NormalizarTitulo(string titulo)
    {
        return titulo.Trim().ToLowerInvariant();
    }
}
