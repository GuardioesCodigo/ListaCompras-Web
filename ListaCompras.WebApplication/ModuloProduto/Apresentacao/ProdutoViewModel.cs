using System.ComponentModel.DataAnnotations;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao;

public record ListarProdutosViewModel(
    string Id,
    string Nome,
    Categoria Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado    
);

public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Categoria\" deve ser selecionado.")]
    string Categoria,

    [Required(ErrorMessage = "O campo \"Unidade de medida\" deve ser preenchido.")]
    [StringLength(20, ErrorMessage = "O campo \"Unidade de medida\" deve conter no máximo 20 caracteres.")]
    string UnidadeDeMedida,

    [Required(ErrorMessage = "O campo \"Preço aproximado\" deve ser preenchido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Preço aproximado\" deve ser maior que 0.")]
    decimal PrecoAproximado
);