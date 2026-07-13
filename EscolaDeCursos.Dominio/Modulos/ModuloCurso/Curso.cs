using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso;

public class Curso : EntidadeBase<Curso>
{
    public string Titulo { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = null!;
    public Nivel Nivel { get; private set; }
    public int CargaHoraria { get; private set; }
    public List<Modulo> Modulos { get; set; } = [];

    // public List<Turma> Turmas { get; set; } = [];
    protected Curso()
    {
    }
    public Curso(
        string titulo,
        string descricao,
        Categoria categoria,
        Nivel nivel,
        int cargaHoraria)
    {
        Titulo = titulo;
        Descricao = descricao;
        Categoria = categoria;
        CategoriaId = categoria.Id;
        Nivel = nivel;
        CargaHoraria = cargaHoraria;
    }

    public override void Atualizar(Curso entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        Descricao = entidadeAtualizada.Descricao;
        Categoria = entidadeAtualizada.Categoria;
        Nivel = entidadeAtualizada.Nivel;
        CargaHoraria = entidadeAtualizada.CargaHoraria;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Titulo\" deve conter entre 2 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao) || Descricao.Length < 2 || Descricao.Length > 100)
            erros.Add("O campo \"Descrição\" deve conter entre 2 e 100 caracteres.");

        if (Categoria is null)
            erros.Add("O campo \"Categoria\" é obrigatório.");

        if (!Enum.IsDefined(Nivel))
            erros.Add("O campo \"Nivel\" deve ser preenchido.");

        if (CargaHoraria <= 0)
            erros.Add("A carga horária deve ser maior que zero.");

        return erros;
    }
}
