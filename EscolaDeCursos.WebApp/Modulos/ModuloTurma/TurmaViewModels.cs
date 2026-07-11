namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public record ListarTurmasViewModel(
    Guid Id,
    string Nome,
    // string CursoNome,
    string InstrutorNome,
    int CapacidadeMaxima,
    DateOnly DataInicio,
    DateOnly DataTermino
);
