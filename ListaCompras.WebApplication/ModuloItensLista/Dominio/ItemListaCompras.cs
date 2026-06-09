using ListaCompras.WebApplication.Compartilhado;

public class ItemListaCompras : EntidadeBase<ItemListaCompras>
{
    public string ProdutoId { get; set; }
    public string NomeProduto { get; set; }
    public string CategoriaProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal Preco { get; set; }

    public ItemListaCompras() { }

    public ItemListaCompras(string produtoId, string nomeProduto, string categoriaNome, int quantidade, decimal preco)
    {
        ProdutoId = produtoId;
        NomeProduto = nomeProduto;
        CategoriaProduto = categoriaNome;
        Quantidade = quantidade;
        Preco = preco;
    }

    // --- Implementação obrigatória da EntidadeBase ---

    public override void AtualizarDados(ItemListaCompras entidadeAtualizada)
    {
        NomeProduto = entidadeAtualizada.NomeProduto;
        CategoriaProduto = entidadeAtualizada.CategoriaProduto;
        Quantidade = entidadeAtualizada.Quantidade;
        Preco = entidadeAtualizada.Preco;
    }

    public override List<string> Validar()
    {
        var erros = new List<string>();

        if (Quantidade <= 0)
            erros.Add("A quantidade deve ser maior que zero.");
        
        if (string.IsNullOrWhiteSpace(NomeProduto))
            erros.Add("O nome do produto é obrigatório.");

        return erros;
    }
}