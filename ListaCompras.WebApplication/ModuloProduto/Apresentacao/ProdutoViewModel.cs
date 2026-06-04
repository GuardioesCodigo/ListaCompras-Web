using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    Categoria Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado    
);