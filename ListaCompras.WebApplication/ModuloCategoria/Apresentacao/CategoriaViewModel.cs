using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.ModuloCategoria.Apresentacao;

public record ListarCategoriasViewModel(
    string Id,
    string Nome,
    CorCategoria Cor
);