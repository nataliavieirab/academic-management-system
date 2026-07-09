using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCategoria;

public class ServicoCategoria
{
    private readonly IRepositorioCategoria repositorioCategoria;

    public ServicoCategoria(
        IRepositorioCategoria repositorioCategoria
    )
    {
        this.repositorioCategoria = repositorioCategoria;
    }

    public List<ListarCategoriasDto> SelecionarTodos()
    {
        return repositorioCategoria
            .SelecionarTodos()
            .Select(c => new ListarCategoriasDto(
                c.Id,
                c.Titulo
            ))
            .ToList();
    }
}
