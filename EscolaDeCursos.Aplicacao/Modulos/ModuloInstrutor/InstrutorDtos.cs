namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public record ListarInstrutoresDto(
  Guid Id,
  string Nome,
  string Cpf,
  string Telefone,
  string Email
);

public record CadastrarInstrutorDto(
    string Nome,
    string Cpf,
    string Telefone,
    string Email
);

public record EditarInstrutorDto(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email
);

public record DetalhesInstrutorDto(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email
);
