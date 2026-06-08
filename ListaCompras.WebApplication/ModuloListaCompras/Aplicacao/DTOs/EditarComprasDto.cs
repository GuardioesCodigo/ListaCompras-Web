using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras.Aplicacao.DTOs;

public record EditarListaComprasDto(string Id, string Nome, StatusListaCompras Status);