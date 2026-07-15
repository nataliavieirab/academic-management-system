using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

namespace EscolaDeCursos.Dominio.Modulos.ModuloAluno;

public interface IRepositorioAluno : IRepositorio<Aluno>
{
    List<Aluno> Selecionar(SituacaoAluno? situacao = null, Guid? cursoId = null);
}
