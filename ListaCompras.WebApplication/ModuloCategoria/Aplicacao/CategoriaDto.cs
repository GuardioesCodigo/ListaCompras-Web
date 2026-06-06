using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloCategoria.Aplicacao;

public record ListarCategoriaDto(
    string Id,
    string Nome,
    CorCategoria Cor
);

public record CadastrarCategoriaDto(
    string Nome,
    CorCategoria Cor
);

public record EditarCategoriaDto(
    string Id,
    string Nome,
    CorCategoria Cor
);

public record DetalhesCategoriaDto(
    string Id,
    string Nome,
    CorCategoria Cor
);

