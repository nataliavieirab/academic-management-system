using EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.Infra.Modulos.ModuloCurso.ModuloModulo;

public sealed class RepositorioModulo(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Modulo>(dbContext), IRepositorioModulo
{
    public override List<Modulo> SelecionarTodos()
    {
        return registros
            .Include(m => m.Curso)
            .OrderBy(m => m.Ordem)
            .ToList();
    }

    public override Modulo? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(m => m.Curso)
            .SingleOrDefault(m => m.Id == idSelecionado);
    }
}
