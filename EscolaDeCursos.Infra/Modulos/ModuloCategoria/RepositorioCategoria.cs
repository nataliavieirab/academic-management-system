using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Infra.Compartilhado.Orm;
namespace EscolaDeCursos.Infra.Modulos.ModuloCategoria;

public sealed class RepositorioCategoria(EscolaDeCursosDbContext dbContext) :
RepositorioBase<Categoria>(dbContext), IRepositorioCategoria
{
    private readonly EscolaDeCursosDbContext db = dbContext;

    public List<Categoria> Selecionar(Guid? cursoId = null)
    {
        IQueryable<Categoria> query = registros;

        bool filtrarCurso = cursoId is not null && cursoId != Guid.Empty;

        if (filtrarCurso)
            query = query.Where(cat => db.Cursos.Any(c => c.Id == cursoId && c.CategoriaId == cat.Id));

        return query.OrderBy(c => c.Titulo).ToList();
    }
}
