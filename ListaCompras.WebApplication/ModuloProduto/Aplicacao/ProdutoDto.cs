using System;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Aplicacao;

public record ListarProdutoDto(
    string Id,
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
    string Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);

public record DetalhesProdutoDto(
    string Id,
    string Nome,
    string CategoriaId,
    string UnidadeMedida,
    decimal PrecoAproximado
);