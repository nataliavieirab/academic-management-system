using System.ComponentModel.DataAnnotations;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCategoria;

public class ListarCategoriasPaginaViewModel
{
    public Guid? CursoId { get; set; }
    public List<OpcaoCursoViewModel> Cursos { get; set; } = [];
    public List<ListarCategoriasViewModel> Categorias { get; set; } = [];
}

public record ListarCategoriasViewModel(
  Guid Id,
  string Titulo
);

public record CadastrarCategoriaViewModel(
    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Categoria\" deve conter entre 2 e 100 caracteres.")]
    string Titulo
);

public record EditarCategoriaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Categoria\" deve conter entre 2 e 100 caracteres.")]
    string Titulo
);

public record ExcluirCategoriaViewModel(
    Guid Id,
    string Titulo
);
