using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Infra.Compartilhado.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloAluno;

public sealed class RepositorioAluno(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Aluno>(dbContext), IRepositorioAluno
{
    private readonly EscolaDeCursosDbContext db = dbContext;

    public List<Aluno> Selecionar(SituacaoAluno? situacao = null, Guid? cursoId = null)
    {
        IQueryable<Aluno> query = registros;

        bool filtrarCurso = cursoId is not null && cursoId != Guid.Empty;

        if (situacao is not null || filtrarCurso)
        {
            query = query.Where(a =>
                db.Matriculas.Any(m =>
                    m.AlunoId == a.Id &&
                    (situacao == null || m.Situacao == situacao) &&
                    (!filtrarCurso || m.Turma!.Curso.Id == cursoId)
                )
            );
        }

        return query.OrderBy(a => a.Nome).ToList();
    }

    public override List<Aluno> SelecionarTodos()
    {
        return registros.OrderBy(a => a.Nome).ToList();
    }
}
