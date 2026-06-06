using System.Text.Json;
using System.Text.Json.Serialization;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaDeCompras.WebApplication.Compartilhado.Infra.Arquivos;
    
public class ContextoJson
{
    public List<Categoria> Categorias { get; set; } = new List<Categoria>();
    public List<Produto> Produtos { get; set; } = new List<Produto>();
    public List<ListaDeCompras> ListaCompras { get; set; } = new List<ListaDeCompras>();
    private readonly string caminhoArquivo;

    public ContextoJson()
    {
        string caminhoAppData = Environment
            .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // MODIFICAÇÃO: Pasta corrigida exclusivamente para o seu sistema de listas atual
        string caminhoDiretorio = Path.Combine(caminhoAppData, "ListaComprasWeb");

        Directory.CreateDirectory(caminhoDiretorio);

        caminhoArquivo = Path.Combine(caminhoDiretorio, "dados.json");

        // Carrega o arquivo do disco rígido assim que a aplicação sobe
        Carregar();
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

        if (string.IsNullOrWhiteSpace(jsonString))
            return;

        JsonSerializerOptions opcoesJson = new JsonSerializerOptions();
        opcoesJson.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opcoesJson.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo = JsonSerializer
            .Deserialize<ContextoJson>(jsonString, opcoesJson);

        if (contextoSalvo == null)
            return;

        Produtos = contextoSalvo.Produtos;
        Categorias = contextoSalvo.Categorias;
        
        // MODIFICAÇÃO: Recupera a lista de compras salva corretamente do JSON
        ListaCompras = contextoSalvo.ListaCompras;
    }
}