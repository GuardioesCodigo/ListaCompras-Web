using System.ComponentModel.DataAnnotations;

public class ItemListaViewModel
{
    public string ListaId { get; set; }

    [Required(ErrorMessage = "Selecione um produto.")]
    public string ProdutoId { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser um número positivo.")]
    public int Quantidade { get; set; }
}