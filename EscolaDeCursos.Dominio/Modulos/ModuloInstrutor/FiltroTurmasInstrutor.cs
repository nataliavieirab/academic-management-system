using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

public enum FiltroTurmasInstrutor
{
    [Display(Name = "Com turmas")]
    ComTurmas,
    [Display(Name = "Sem turmas")]
    SemTurmas
}
