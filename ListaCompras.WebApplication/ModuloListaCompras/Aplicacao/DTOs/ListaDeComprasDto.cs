using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;

public record ListarListaComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status, // Ajuste para o tipo que você usa (Enum ou string)
    int TotalItens,
    decimal TotalGasto
);

public record CadastrarListaComprasDto(
    string Nome
);

public record EditarListaComprasDto(
    string Id,
    string Nome,
    StatusListaCompras Status
);

public record DetalhesListaComprasDto(
    string Id,
    string Nome,
    DateTime DataCriacao,
    StatusListaCompras Status,
    int TotalItens,
    decimal TotalGasto
);