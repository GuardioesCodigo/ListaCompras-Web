using System;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Aplicacao;



public record DetalhesProdutoDto(
    string Id,
    string Nome,
    Categoria Categoria,
    string UnidadeMedida,
    decimal PrecoAproximado
);
