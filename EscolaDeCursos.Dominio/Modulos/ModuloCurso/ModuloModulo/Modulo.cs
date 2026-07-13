using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;

public class Modulo : EntidadeBase<Modulo>
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public int Duracao { get; set; }
    public Guid CursoId { get; set; }
    public Curso Curso { get; set; } = null!;

    protected Modulo()
    {
    }
    public Modulo(
        string titulo,
        string descricao,
        int ordem,
        int duracao,
        Curso curso)
    {
        Titulo = titulo;
        Descricao = descricao;
        Ordem = ordem;
        Duracao = duracao;
        Curso = curso;
        CursoId = curso.Id;
    }

    public override void Atualizar(Modulo entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        Descricao = entidadeAtualizada.Descricao;
        Ordem = entidadeAtualizada.Ordem;
        Duracao = entidadeAtualizada.Duracao;
        Curso = entidadeAtualizada.Curso;
        CursoId = entidadeAtualizada.CursoId;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Título\" deve conter entre 2 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 2 || Descricao.Length > 100)
            erros.Add("O campo \"Descrição\" deve conter entre 2 e 100 caracteres.");

        if (Ordem <= 0)
            erros.Add("A ordem do módulo deve ser maior que zero.");

        if (Duracao <= 0)
            erros.Add("A duração do módulo deve ser maior que zero.");

        if (CursoId == Guid.Empty)
            erros.Add("O curso é obrigatório.");

        return erros;
    }
}
