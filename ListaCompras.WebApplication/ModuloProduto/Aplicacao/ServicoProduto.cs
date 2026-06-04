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

        if (ExisteProdutoComNome(dto.Nome))
            return Falha("Nome", "Já existe um produto dentro desta categoria com este nome.");

        Produto novoProduto = new Produto(
            dto.Nome,
            dto.UnidadeMedida,
            dto.PrecoAproximado,
            dto.Categoria
        );

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        if (ExisteProdutoComNome(dto.Nome))
            return Falha("Nome", "Já existe um produto dentro desta categoria com este nome.");

        Produto produtoAtualizado = new Produto(
            dto.Nome,
            dto.UnidadeMedida,
            dto.PrecoAproximado,
            dto.Categoria
        );

        bool conseguiuEditar = repositorioProduto.Editar(dto.Id, produtoAtualizado);

        if (!conseguiuEditar)
            return Result.Fail("Produto não encontrado.");

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

    public bool ExisteProdutoComNome(string nome, string? idIgnorado = null)
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        foreach (Produto p in produtos)
        {
            if (p.Id != idIgnorado && string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static Result Falha(string campo, string mensagem)
    {
        IError erro = new Error(mensagem).WithMetadata("Campo", campo);

        return Result.Fail(erro);
    }
}
