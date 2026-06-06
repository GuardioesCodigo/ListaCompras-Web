namespace ListaCompras.Application.DTOs;

public class ItemListaComprasDto
{
    public string Id { get; set; }
    public string ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public string CategoriaProduto { get; set; } // Atende ao requisito de exibir categoria
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal PrecoTotal => PrecoUnitario * Quantidade;
}