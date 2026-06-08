using System.ComponentModel.DataAnnotations;

namespace ListaCompras.WebApplication.ModuloListaCompras.Apresentacao;

public record ListaComprasFormViewModel(
    string? Id,

    [Required(ErrorMessage = "O nome da lista é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve conter de 3 a 100 caracteres.")]
    string Nome
);