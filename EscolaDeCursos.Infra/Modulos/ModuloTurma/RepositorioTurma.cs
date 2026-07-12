using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloTurma;

public sealed class RepositorioTurma(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Turma>(dbContext), IRepositorioTurma
{
    public override List<Turma> SelecionarTodos()
    {
        return registros
    .Include(t => t.Instrutor)
    .OrderBy(t => t.Nome)
    .ToList();
    }
}