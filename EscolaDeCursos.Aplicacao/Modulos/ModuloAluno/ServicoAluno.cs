using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public class ServicoAluno
{
    private readonly IRepositorioAluno repositorioAluno;

    public ServicoAluno(
        IRepositorioAluno repositorioAluno
    )
    {
        this.repositorioAluno = repositorioAluno;
    }

    public List<ListarAlunosDto> SelecionarTodos()
    {
        return repositorioAluno
            .SelecionarTodos()
            .Select(a => new ListarAlunosDto(
                a.Id,
                a.Nome,
                a.Cpf,
                a.Telefone,
                a.Email,
                a.Endereco
            ))
            .ToList();
    }
}