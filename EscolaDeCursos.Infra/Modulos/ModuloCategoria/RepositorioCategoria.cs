using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Infra.Compartilhado.Orm;
namespace EscolaDeCursos.Infra.Modulos.ModuloCategoria;

public sealed class RepositorioCategoria(EscolaDeCursosDbContext dbContext) :
RepositorioBase<Categoria>(dbContext), IRepositorioCategoria
{
}
