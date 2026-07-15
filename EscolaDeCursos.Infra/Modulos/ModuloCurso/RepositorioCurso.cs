using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;
namespace EscolaDeCursos.Infra.Modulos.ModuloCurso;

public sealed class RepositorioCurso(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Curso>(dbContext), IRepositorioCurso
{
    private readonly EscolaDeCursosDbContext db = dbContext;

    public List<Curso> Selecionar(
        Guid? categoriaId = null,
        Nivel? nivel = null,
        FiltroTurmasCurso? filtroTurmas = null)
    {
        IQueryable<Curso> query = registros.Include(c => c.Categoria);

        bool filtrarCategoria = categoriaId is not null && categoriaId != Guid.Empty;

        if (filtrarCategoria)
            query = query.Where(c => c.CategoriaId == categoriaId);

        if (nivel is not null)
            query = query.Where(c => c.Nivel == nivel);

        if (filtroTurmas == FiltroTurmasCurso.ComTurmas)
            query = query.Where(c => db.Turmas.Any(t => t.Curso.Id == c.Id));
        else if (filtroTurmas == FiltroTurmasCurso.SemTurmas)
            query = query.Where(c => !db.Turmas.Any(t => t.Curso.Id == c.Id));

        return query.ToList();
    }

    public override List<Curso> SelecionarTodos()
    {
        return registros
            .Include(c => c.Categoria)
            .ToList();
    }

    public override Curso? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(c => c.Categoria)
            .SingleOrDefault(c => c.Id == idSelecionado);
    }
}
