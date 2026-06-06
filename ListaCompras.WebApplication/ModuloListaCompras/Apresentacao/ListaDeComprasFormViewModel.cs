using System.ComponentModel.DataAnnotations;

namespace ListaCompras.WebApplication.ModuloListaCompras;

public class ListaDeComprasFormViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O nome da lista é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve conter de 3 a 100 caracteres.")]
    public string Nome { get; set; }
}