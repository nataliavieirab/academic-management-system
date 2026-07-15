using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.Dominio.Modulos.ModuloTurma;

public enum FiltroCapacidadeTurma
{
    [Display(Name = "Lotadas")]
    Lotadas,
    [Display(Name = "Com vagas")]
    ComVagas
}
