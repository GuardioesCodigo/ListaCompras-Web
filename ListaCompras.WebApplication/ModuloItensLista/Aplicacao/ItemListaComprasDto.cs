namespace ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;

public record ItemListaComprasDto(
    string Id,
    string ProdutoId,
    string NomeProduto,
    string CategoriaProduto,
    int Quantidade,
    decimal PrecoUnitario
)
{
    // O PrecoTotal é calculado automaticamente
    public decimal PrecoTotal => PrecoUnitario * Quantidade;
}