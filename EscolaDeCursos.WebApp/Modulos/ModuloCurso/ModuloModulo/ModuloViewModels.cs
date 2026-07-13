namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.ModuloModulo;

public record ListarModulosViewModel(
    Guid Id,
    string Titulo,
    int Ordem,
    int Duracao,
    Guid CursoId,
    string CursoTitulo
);
