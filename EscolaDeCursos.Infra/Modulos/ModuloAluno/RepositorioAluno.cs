using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Infra.Compartilhado.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloAluno;

public sealed class RepositorioAluno(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Aluno>(dbContext), IRepositorioAluno
{
    public override List<Aluno> SelecionarTodos()
    {
        return registros.OrderBy(a => a.Nome).ToList();
    }
}