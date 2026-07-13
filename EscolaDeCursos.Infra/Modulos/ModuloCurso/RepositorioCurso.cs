using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;
namespace EscolaDeCursos.Infra.Modulos.ModuloCurso;

public sealed class RepositorioCurso(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Curso>(dbContext), IRepositorioCurso
{
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
