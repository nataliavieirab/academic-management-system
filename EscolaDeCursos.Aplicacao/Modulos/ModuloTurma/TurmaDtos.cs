namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public record ListarTurmasDto(
    Guid Id,
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record CadastrarTurmaDto(
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record EditarTurmaDto(
    Guid Id,
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record ExcluirTurmaDto(
    Guid Id,
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record DetalhesTurmaDto(
    Guid Id,
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);
