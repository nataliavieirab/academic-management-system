using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Infra.Compartilhado.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloInstrutor;

public sealed class RepositorioInstrutor(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Instrutor>(dbContext), IRepositorioInstrutor
{
    private readonly EscolaDeCursosDbContext db = dbContext;

    public List<Instrutor> Selecionar(FiltroTurmasInstrutor? filtroTurmas = null)
    {
        IQueryable<Instrutor> query = registros;

        if (filtroTurmas == FiltroTurmasInstrutor.ComTurmas)
            query = query.Where(i => db.Turmas.Any(t => t.Instrutor.Id == i.Id));
        else if (filtroTurmas == FiltroTurmasInstrutor.SemTurmas)
            query = query.Where(i => !db.Turmas.Any(t => t.Instrutor.Id == i.Id));

        return query.OrderBy(i => i.Nome).ToList();
    }

    public override List<Instrutor> SelecionarTodos()
    {
        return registros.OrderBy(i => i.Nome).ToList();
    }
}
