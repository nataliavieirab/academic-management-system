using EscolaDeCursos.Aplicacao.Compartilhado;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using FluentResults;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCategoria;

public class ServicoCategoria : ServicoBase<Categoria>
{
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly IRepositorioCurso repositorioCurso;
    public ServicoCategoria(
        IRepositorioCategoria repositorioCategoria,
        IRepositorioCurso repositorioCurso
    )
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioCurso = repositorioCurso;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComMesmoTitulo(dto.Titulo))
            return Falha(nameof(dto.Titulo), "Já existe uma categoria com este título.");

        Categoria novaCategoria = new Categoria(dto.Titulo);

        Result resultadoValidacao = ValidarEntidade(novaCategoria);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComMesmoTitulo(dto.Titulo, dto.Id))
            return Falha(nameof(dto.Titulo), "Já existe uma categoria com este título.");

        Categoria categoriaAtualizada = new Categoria(dto.Titulo);

        Result resultadoValidacao = ValidarEntidade(categoriaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Categoria não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Falha(string.Empty, "Categoria não encontrada.");

        if (PossuiCursosVinculados(id))
            return Falha(string.Empty, $"Não é possível excluir a categoria \"{categoria.Titulo}\", pois ela possui cursos vinculados.");

        repositorioCategoria.Excluir(id);

        return Result.Ok();
    }

    public List<ListarCategoriasDto> Selecionar(Guid? cursoId = null)
    {
        return repositorioCategoria
            .Selecionar(cursoId)
            .Select(c => new ListarCategoriasDto(
                c.Id,
                c.Titulo
            ))
            .ToList();
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        return Selecionar();
    }

    public List<OpcaoCursoDto> SelecionarCursos()
    {
        return repositorioCurso
            .SelecionarTodos()
            .Select(c => new OpcaoCursoDto(c.Id, c.Titulo))
            .ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(Guid id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(new DetalhesCategoriaDto(categoria.Id, categoria.Titulo));
    }

    private bool ExisteCategoriaComMesmoTitulo(string titulo, Guid? idIgnorado = null)
    {
        string tituloNormalizado = NormalizarTitulo(titulo);

        return repositorioCategoria
            .SelecionarTodos()
            .Any(c =>
                c.Id != idIgnorado &&
                NormalizarTitulo(c.Titulo) == tituloNormalizado
            );
    }

    private bool PossuiCursosVinculados(Guid categoriaId)
    {
        return repositorioCurso
            .SelecionarTodos()
            .Any(curso => curso.CategoriaId == categoriaId);
    }

    private static string NormalizarTitulo(string titulo)
    {
        return titulo.Trim().ToLowerInvariant();
    }

}
