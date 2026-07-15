using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloTurma;

public sealed class RepositorioTurma(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Turma>(dbContext), IRepositorioTurma
{
    private readonly EscolaDeCursosDbContext db = dbContext;

    public List<Turma> Selecionar(Guid? cursoId = null, FiltroCapacidadeTurma? filtroCapacidade = null)
    {
        IQueryable<Turma> query = registros
            .Include(t => t.Instrutor)
            .Include(t => t.Curso);

        bool filtrarCurso = cursoId is not null && cursoId != Guid.Empty;

        if (filtrarCurso)
            query = query.Where(t => t.Curso.Id == cursoId);

        if (filtroCapacidade == FiltroCapacidadeTurma.Lotadas)
        {
            query = query.Where(t =>
                db.Matriculas.Count(m =>
                    m.TurmaId == t.Id &&
                    m.Situacao == SituacaoAluno.Ativa
                ) >= t.CapacidadeMaxima);
        }
        else if (filtroCapacidade == FiltroCapacidadeTurma.ComVagas)
        {
            query = query.Where(t =>
                db.Matriculas.Count(m =>
                    m.TurmaId == t.Id &&
                    m.Situacao == SituacaoAluno.Ativa
                ) < t.CapacidadeMaxima);
        }

        return query.OrderBy(t => t.Nome).ToList();
    }

    public override Turma? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(t => t.Instrutor)
            .Include(t => t.Curso)
            .SingleOrDefault(t => t.Id == idSelecionado);
    }

    public override List<Turma> SelecionarTodos()
    {
        return registros
            .Include(t => t.Instrutor)
            .Include(t => t.Curso)
            .OrderBy(t => t.Nome)
            .ToList();
    }
}
