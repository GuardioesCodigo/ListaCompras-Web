using System.ComponentModel.DataAnnotations;

namespace ListaCompras.WebApplication.ModuloItensLista;

public class ItemListaFormViewModel
{
    [Required]
    public string ListaComprasId { get; set; }

    [Required(ErrorMessage = "A seleção de um produto é obrigatória.")]
    public string ProdutoId { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade informada deve ser positiva.")]
    public int Quantidade { get; set; }
}