using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso;

public enum FiltroTurmasCurso
{
    [Display(Name = "Com turmas")]
    ComTurmas,
    [Display(Name = "Sem turmas")]
    SemTurmas
}
