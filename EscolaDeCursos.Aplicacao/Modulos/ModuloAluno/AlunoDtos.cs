namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public record ListarAlunosDto(
  Guid Id,
  string Nome,
  string Cpf,
  string Telefone,
  string Email,
  string Endereco
);

public record CadastrarAlunoDto(
    string Nome,
    string Cpf,
    string Telefone,
    string Email,
    string Endereco
);

public record EditarAlunoDto(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email,
    string Endereco
);

public record ExcluirAlunoDto(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email,
    string Endereco
);

public record DetalhesAlunoDto(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email,
    string Endereco
);