using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloMatricula;

public sealed class RepositorioMatricula(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Matricula>(dbContext), IRepositorioMatricula
{
    public List<Matricula> Selecionar(Guid turmaId, SituacaoAluno? situacao = null)
    {
        IQueryable<Matricula> query = registros
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .Where(m => m.TurmaId == turmaId);

        if (situacao is not null)
            query = query.Where(m => m.Situacao == situacao);

        return query.OrderByDescending(m => m.Data).ToList();
    }

    public override Matricula? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .SingleOrDefault(m => m.Id == idSelecionado);
    }

    public override List<Matricula> SelecionarTodos()
    {
        return registros
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .OrderByDescending(m => m.Data)
            .ToList();
    }
}
