using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor;

public record ListarInstrutoresViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Telefone,
    string Email
);