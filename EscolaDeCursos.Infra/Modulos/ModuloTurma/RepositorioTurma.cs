using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloTurma;

public sealed class RepositorioTurma(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Turma>(dbContext), IRepositorioTurma
{
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