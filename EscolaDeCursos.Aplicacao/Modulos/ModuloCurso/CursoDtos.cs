using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public record ListarCursosDto(
    Guid Id,
    string Titulo,
    Guid CategoriaId,
    string CategoriaTitulo,
    Nivel Nivel,
    int CargaHoraria
);

public record CadastrarCursoDto(
    string Titulo,
    string Descricao,
    Guid CategoriaId,
    Nivel Nivel,
    int CargaHoraria
);

public record EditarCursoDto(
    Guid Id,
    string Titulo,
    string Descricao,
    Guid CategoriaId,
    Nivel Nivel,
    int CargaHoraria
);

public record DetalhesCursoDto(
    Guid Id,
    string Titulo,
    string Descricao,
    Guid CategoriaId,
    string CategoriaTitulo,
    Nivel Nivel,
    int CargaHoraria
);
