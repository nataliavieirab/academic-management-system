using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.ModuloModulo;

public record ListarModulosViewModel(
    Guid Id,
    string Titulo,
    string Descricao,
    int Duracao,
    Guid CursoId,
    string CursoTitulo
);

public record CadastrarModuloViewModel(

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,

    [Range(1, int.MaxValue, ErrorMessage = "A ordem do módulo deve ser maior que zero.")]
    int Ordem,

    [Range(1, int.MaxValue, ErrorMessage = "A duração do módulo deve ser maior que zero.")]
    int Duracao,

    [Required(ErrorMessage = "O campo \"Curso\" deve ser preenchido.")]
    Guid CursoId
);

public record EditarModuloViewModel(

    Guid Id,

    [Required(ErrorMessage = "O campo \"Título\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" deve conter entre 2 e 100 caracteres.")]
    string Titulo,

    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,

    [Range(1, int.MaxValue, ErrorMessage = "A ordem do módulo deve ser maior que zero.")]
    int Ordem,

    [Range(1, int.MaxValue, ErrorMessage = "A duração do módulo deve ser maior que zero.")]
    int Duracao,

    [Required(ErrorMessage = "O campo \"Curso\" deve ser preenchido.")]
    Guid CursoId
);

public record ExcluirModuloViewModel(
    Guid Id,
    string Titulo,
    string Descricao,
    int Ordem,
    int Duracao,
    Guid CursoId,
    string CursoTitulo
);
