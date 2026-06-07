using System.Text.Json;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

public class ContextoJson
{
    public Guid IdInstancia { get; } = Guid.NewGuid(); 
    public List<Categoria> Categorias { get; set; } = new();
    public List<Produto> Produtos { get; set; } = new();
    public List<ListaDeCompras> ListaCompras { get; set; } = new();
    
    private readonly string caminhoArquivo;

    public ContextoJson()
    {
        // Usando uma pasta fixa para teste (garantir que não é erro de permissão)
        string pastaTeste = @"C:\TEMP";
        if (!Directory.Exists(pastaTeste)) Directory.CreateDirectory(pastaTeste);
        
        caminhoArquivo = Path.Combine(pastaTeste, "dados.json");
        Console.WriteLine($"DEBUG: CONTEXTO CRIADO. ID: {IdInstancia} | Caminho: {caminhoArquivo}");
        
        Carregar();
    }

 public void Salvar()
{
    try 
    {
        // 1. Forçamos um caminho fixo e absoluto para não haver erro de permissão
        string caminho = @"C:\dados_lista.json"; 
        
        string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        
        // 2. Escreve o arquivo
        File.WriteAllText(caminho, jsonString);
        
        // 3. Log de sucesso
        Console.WriteLine($"DEBUG: ARQUIVO GERADO COM SUCESSO EM: {caminho}");
    }
    catch (Exception ex) 
    {
        // 4. Se falhar, o erro aparecerá no terminal
        Console.WriteLine($"DEBUG: ERRO CRÍTICO AO SALVAR JSON: {ex.Message}");
    }
}

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivo)) return;

        try {
            string jsonString = File.ReadAllText(caminhoArquivo);
            var contextoSalvo = JsonSerializer.Deserialize<ContextoJson>(jsonString);
            if (contextoSalvo != null) {
                this.ListaCompras = contextoSalvo.ListaCompras;
                Console.WriteLine($"DEBUG: CARREGADO com sucesso. Total: {ListaCompras.Count} itens.");
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"DEBUG: ERRO AO CARREGAR: {ex.Message}");
        }
    }
}