using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Infra.Compartilhado.Orm;

namespace EscolaDeCursos.Infra.Modulos.ModuloInstrutor;

public sealed class RepositorioInstrutor(EscolaDeCursosDbContext dbContext) :
    RepositorioBase<Instrutor>(dbContext), IRepositorioInstrutor
{
    public override List<Instrutor> SelecionarTodos()
    {
        return registros.OrderBy(i => i.Nome).ToList();
    }
}