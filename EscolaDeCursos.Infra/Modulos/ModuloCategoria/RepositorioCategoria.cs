using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

public sealed class RepositorioCategoria(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Categoria>(dbContext), IRepositorioCategoria
{
    public override List<Categoria> SelecionarTodos()
    {
        return registros.OrderBy(c => c.Titulo).ToList();
    }
}
