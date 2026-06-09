using System.Text.Json;
using System.Text.Json.Serialization;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloProduto;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio;
using ListaCompras.WebApplication.ModuloItensLista.Apresentacao.Controllers;


namespace ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

public class ContextoJson
{

    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    public List<Produto> Produtos { get; set; } = new List<Produto>();
    public List<ListaDeCompras> ListaCompras {get; set;} = new List<ListaDeCompras>();
    public List<ItemListaCompras> ItemListaCompras {get; set;} = new List<ItemListaCompras>();
    private readonly string caminhoArquivo;

    public ContextoJson()
    {
        
        Categorias = new List<Categoria>();
        Produtos = new List<Produto>();


        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorio = Path.Combine(caminhoAppData, "ClubeDaLeituraWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");
    }
    
    public void Salvar()
    {

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.WriteIndented = true;
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, opcoesJson);

        File.WriteAllText(caminhoArquivo, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo))
            return;

        string jsonString = File.ReadAllText(caminhoArquivo);

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo = JsonSerializer
            .Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo == null)
            return;

        Produtos = contextoSalvo.Produtos;
        Categorias = contextoSalvo.Categorias;
        ListaCompras = contextoSalvo.ListaCompras;
        ItemListaCompras = contextoSalvo.ItemListaCompras;
    }
}

   

