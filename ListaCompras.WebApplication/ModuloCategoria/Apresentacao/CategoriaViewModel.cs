using System.ComponentModel.DataAnnotations;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    Guid Id,
    string Nome,
    CorCategoria Cor
);

public record CadastrarCategoriasViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Cor\" deve ser selecionado.")]
    CorCategoria Cor
);

public record EditarCategoriasViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, ErrorMessage = "O campo \"Nome\" deve conter no máximo 50 caracteres.")]
    string Nome,

    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Cor\" deve ser selecionado.")]
    CorCategoria Cor
);

public record ExcluirCategoriasViewModel(
    Guid Id,
    string Nome,
    CorCategoria Cor
);