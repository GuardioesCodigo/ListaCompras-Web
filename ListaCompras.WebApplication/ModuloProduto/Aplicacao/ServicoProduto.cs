using System;
using FluentResults;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Aplicacao;

public class ServicoProduto
{
    private readonly IRepositorioProduto repositorioProduto;

    public ServicoProduto(IRepositorioProduto repositorioProduto)
    {
        this.repositorioProduto = repositorioProduto;
    }

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        Produto novoProduto = new Produto(
            dto.Nome,
            dto.UnidadeMedida,
            dto.PrecoAproximado,
            dto.Categoria
        );

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public List<ListarProdutoDto> SelecionarTodos()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        return produtos
            .Select(p => new ListarProdutoDto(p.Id, p.Nome, p.Categoria, p.UnidadeMedida, p.PrecoAproximado))
            .ToList();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(string id)
    {
        Produto? produtos = repositorioProduto.SelecionarPorId(id);

        if (produtos == null)
            return Result.Fail("Produto não encontrada.");

        return Result.Ok(
            new DetalhesProdutoDto(produtos.Id, produtos.Nome, produtos.Categoria, produtos.UnidadeMedida, produtos.PrecoAproximado)
        );
    }
}
