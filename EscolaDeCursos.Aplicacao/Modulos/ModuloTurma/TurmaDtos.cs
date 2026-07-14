namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public record OpcaoCursoDto(
    Guid Id,
    string Titulo
);

public record OpcaoInstrutorDto(
    Guid Id,
    string Nome
);

public record ListarTurmasDto(
    Guid Id,
    string Nome,
    string CursoTitulo,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record CadastrarTurmaDto(
    string Nome,
    Guid CursoId,
    Guid InstrutorId,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record EditarTurmaDto(
    Guid Id,
    string Nome,
    Guid CursoId,
    Guid InstrutorId,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record ExcluirTurmaDto(
    Guid Id,
    string Nome,
    Guid CursoId,
    Guid InstrutorId,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);

public record DetalhesTurmaDto(
    Guid Id,
    string Nome,
    Guid CursoId,
    string CursoTitulo,
    Guid InstrutorId,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);
