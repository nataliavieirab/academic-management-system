using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public record OpcaoInstrutorViewModel(
    Guid Id,
    string Nome
);

public record OpcaoCursoViewModel(
    Guid Id,
    string Nome
);
public record ListarTurmasViewModel(
    Guid Id,
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record CadastrarTurmaViewModel(
    [Required(ErrorMessage = "O campo \"Nome da Turma\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome da Turma\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    // [Required(ErrorMessage = "O campo \"Curso\" deve ser selecionado.")]
    // Guid CursoId,

    [Required(ErrorMessage = "O campo \"Instrutor\" deve ser selecionado.")]
    Guid? InstrutorId,

    [Required(ErrorMessage = "O campo \"Capacidade Máxima\" deve ser preenchido.")]
    [Range(1, 100, ErrorMessage = "A capacidade deve ser entre 1 e 100 alunos.")]
    int CapacidadeMaxima,

    [Required(ErrorMessage = "O campo \"Data de Início\" deve ser preenchido.")]
    DateOnly DataInicio,

    [Required(ErrorMessage = "O campo \"Data de Término\" deve ser preenchido.")]
    DateOnly DataTermino,

    // [ValidateNever]
    // List<OpcaoCursoViewModel> Cursos,

    [ValidateNever]
    List<OpcaoInstrutorViewModel> Instrutores

);

public record EditarTurmaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome da Turma\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2,
        ErrorMessage = "O campo \"Nome da Turma\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Instrutor\" deve ser selecionado.")]
    Guid? InstrutorId,

    [Required(ErrorMessage = "O campo \"Capacidade Máxima\" deve ser preenchido.")]
    [Range(1, 100, ErrorMessage = "A capacidade deve ser entre 1 e 100 alunos.")]
    int CapacidadeMaxima,

    [Required(ErrorMessage = "O campo \"Data de Início\" deve ser preenchido.")]
    DateOnly DataInicio,

    [Required(ErrorMessage = "O campo \"Data de Término\" deve ser preenchido.")]
    DateOnly DataTermino,

    [ValidateNever]
    List<OpcaoInstrutorViewModel> Instrutores
);

public record ExcluirTurmaViewModel(
    Guid Id,
    string Nome,
    Guid? InstrutorId,
    string? InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

