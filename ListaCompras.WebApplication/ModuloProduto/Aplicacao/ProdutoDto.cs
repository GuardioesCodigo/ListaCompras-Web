using System;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Aplicacao;

public record ListarProdutoDto(
    Guid Id,
    string Nome,
    string CategoriaNome,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record CadastrarProdutoDto(   
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record EditarProdutoDto(
    Guid Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record DetalhesProdutoDto(
    Guid Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);
