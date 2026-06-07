using System.Text.Json;
using System.Text.Json.Serialization;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

public class ContextoJson
{
    public List<Categoria> Categorias { get; set; } = new();
    public List<Produto> Produtos { get; set; } = new();
    public List<ListaDeCompras> ListaCompras { get; set; } = new();

    private readonly string _caminhoArquivo;

    public ContextoJson()
    {
        string pasta = @"C:\TEMP";
        if (!Directory.Exists(pasta)) Directory.CreateDirectory(pasta);
        _caminhoArquivo = Path.Combine(pasta, "dados_lista.json");
        
        // AQUI ESTÁ O PULO DO GATO:
        // Não chamamos Carregar() aqui dentro do construtor, 
        // pois isso está gerando a recursão infinita na inicialização do serviço.
    }

    public void Salvar()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(_caminhoArquivo, jsonString);
    }

    // Chamaremos o carregar apenas quando realmente precisarmos
    public void Carregar()
    {
        if (!File.Exists(_caminhoArquivo)) return;

        string jsonString = File.ReadAllText(_caminhoArquivo);
        var carregado = JsonSerializer.Deserialize<ContextoJson>(jsonString);
        
        if (carregado != null) 
        {
            this.Categorias = carregado.Categorias;
            this.Produtos = carregado.Produtos;
            this.ListaCompras = carregado.ListaCompras;
        }
    }
}