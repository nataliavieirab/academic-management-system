using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

public interface IRepositorioInstrutor : IRepositorio<Instrutor>
{
    List<Instrutor> Selecionar(FiltroTurmasInstrutor? filtroTurmas = null);
}
