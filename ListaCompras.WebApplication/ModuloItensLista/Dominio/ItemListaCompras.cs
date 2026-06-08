using System.Text.Json.Serialization;
using ListaCompras.WebApplication.ModuloProduto.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras.Dominio;

public class ItemListaCompras
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public Produto Produto { get; set; } = new();
    
    public int Quantidade { get; set; }
    
    public decimal Preco { get; set; }

    // O [JsonIgnore] é o que impede o loop infinito de serialização (Stack Overflow)
    // Ele diz ao sistema para ignorar esta propriedade ao ler/salvar no JSON
    [JsonIgnore]
    public ListaDeCompras? Lista { get; set; }

    // Construtor vazio necessário para a desserialização do JSON
    public ItemListaCompras()
    {
    }

    // Construtor principal para criar o item associado ao produto
    public ItemListaCompras(Produto produto, int quantidade,decimal preco)
    {
        Produto = produto;
        Quantidade = quantidade;
        Preco = preco;
    }
}