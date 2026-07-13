namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso.ModuloModulo;

public record ListarModulosDto(
    Guid Id,
    string Titulo,
    string Descricao,
    int Ordem,
    int Duracao,
    Guid CursoId,
    string CursoTitulo
);

public record CadastrarModuloDto(
    string Titulo,
    string Descricao,
    int Ordem,
    int Duracao,
    Guid CursoId
);

public record EditarModuloDto(
    Guid Id,
    string Titulo,
    string Descricao,
    int Ordem,
    int Duracao,
    Guid CursoId
);

public record DetalhesModuloDto(
    Guid Id,
    string Titulo,
    string Descricao,
    int Ordem,
    int Duracao,
    Guid CursoId,
    string CursoTitulo
);
