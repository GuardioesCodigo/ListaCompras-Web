using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloCategoria.Aplicacao;

public record ListarCategoriaDto(
    Guid Id,
    string Nome,
    CorCategoria Cor
);

public record CadastrarCategoriaDto(
    string Nome,
    CorCategoria Cor
);

public record EditarCategoriaDto(
    Guid Id,
    string Nome,
    CorCategoria Cor
);

public record DetalhesCategoriaDto(
    Guid Id,
    string Nome,
    CorCategoria Cor
);

