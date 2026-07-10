using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public record ListarAlunosViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email,
    string Endereco
);

public record CadastrarAlunoViewModel(
    [Required(ErrorMessage = "O campo \"Aluno\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Aluno\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}-\d{2})$", ErrorMessage = "O campo \"CPF\" deve conter 11 dígitos.")]
    string Cpf,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$", ErrorMessage = "O campo \"Telefone\" deve conter entre 10 e 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Email\" deve ser preenchido.")]
    [EmailAddress(ErrorMessage = "O campo \"Email\" possui formato inválido.")]
    string Email,

    [Required(ErrorMessage = "O campo \"Endereço\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Endereço\" deve conter entre 2 e 100 caracteres.")]
    string Endereco
);

