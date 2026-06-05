using System.ComponentModel.DataAnnotations;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado    
);

public class CadastrarProdutoViewModel{

    public string Id { get; set; } = string.Empty;
    public string? Nome { get; set; }
    public string? CategoriaId { get; set; }
    public string? UnidadeMedida { get; set; }
    public decimal PrecoAproximado { get; set; }
}

public record EditarProdutoViewModel{
    public string Id { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string CategoriaId { get; set; } = string.Empty;
    public string UnidadeMedida { get; set; } = string.Empty;
    public decimal PrecoAproximado { get; set; } = 0;
}

public record ExcluirProdutosViewModel(
    string Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado    
);