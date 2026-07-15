using EscolaDeCursos.Dominio.Compartilhado;
namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso;

public interface IRepositorioCurso : IRepositorio<Curso>
{
    List<Curso> Selecionar(
        Guid? categoriaId = null,
        Nivel? nivel = null,
        FiltroTurmasCurso? filtroTurmas = null);
}
