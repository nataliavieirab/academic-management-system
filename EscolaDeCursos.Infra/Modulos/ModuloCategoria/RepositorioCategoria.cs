using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Infra.Compartilhado.Orm;
public sealed class RepositorioCategoria(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Categoria>(dbContext), IRepositorioCategoria
{
}
