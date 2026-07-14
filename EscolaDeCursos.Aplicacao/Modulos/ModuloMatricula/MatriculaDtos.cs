using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public record OpcaoAlunoDto(
    Guid Id,
    string Nome
);

public record ListarMatriculasDto(
    Guid Id,
    Guid TurmaId,
    string AlunoNome,
    DateOnly Data,
    SituacaoAluno Situacao
);

public record CadastrarMatriculaDto(
    Guid AlunoId,
    Guid TurmaId
);

public record EditarMatriculaDto(
    Guid Id,
    SituacaoAluno Situacao
);

public record DetalhesMatriculaDto(
    Guid Id,
    Guid AlunoId,
    string AlunoNome,
    Guid TurmaId,
    string TurmaNome,
    DateOnly Data,
    SituacaoAluno Situacao
);
