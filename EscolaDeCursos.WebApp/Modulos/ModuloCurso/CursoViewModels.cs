using System.ComponentModel.DataAnnotations;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public record ListarCursosViewModel(
    Guid Id,
    string Titulo,
    Guid CategoriaId,
    string CategoriaTitulo,
    Nivel Nivel,
    int CargaHoraria
);

public record CadastrarCursoViewModel(

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    Guid CategoriaId,

    [Required(ErrorMessage = "O campo \"Nível\" deve ser preenchido.")]
    Nivel Nivel,

    [Range(1, int.MaxValue, ErrorMessage = "A carga horária deve ser maior que zero.")]
    int CargaHoraria
)
{
    public List<SelectListItem> CategoriasDisponiveis { get; set; } = [];
}

public record EditarCursoViewModel(

    Guid Id,

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser preenchido.")]
    Guid CategoriaId,

    [Required(ErrorMessage = "O campo \"Nível\" deve ser preenchido.")]
    Nivel Nivel,

    [Range(1, int.MaxValue, ErrorMessage = "A carga horária deve ser maior que zero.")]
    int CargaHoraria
)
{
    public List<SelectListItem> CategoriasDisponiveis { get; set; } = [];
}

public record ExcluirCursoViewModel(
    Guid Id,
    string Titulo,
    string Descricao,
    Guid CategoriaId,
    string CategoriaTitulo,
    Nivel Nivel,
    int CargaHoraria
);
