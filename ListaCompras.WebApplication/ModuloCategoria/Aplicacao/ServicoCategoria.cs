using FluentResults;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.ModuloCategoria.Apresentacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloProduto;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorio<Categoria> repositorioCategoria;
    private readonly IRepositorio<Produto> repositorioProduto;

    public ServicoCategoria(IRepositorio<Categoria> repositorio, IRepositorio<Produto> repositorioProduto)
    {
        this.repositorioCategoria = repositorio;
        this.repositorioProduto = repositorioProduto;
    }

    public Result Cadastrar(CadastrarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome))
            return Falha("Nome", "Já existe uma categoria com este nome.");

        Categoria novaCategoria = new Categoria(
            dto.Nome,
            dto.Cor
        );

        repositorioCategoria.Cadastrar(novaCategoria);

        return Result.Ok();
    }

    public Result Editar(EditarCategoriaDto dto)
    {
        if (ExisteCategoriaComNome(dto.Nome, dto.Id))
            return Falha("Nome", "Já existe uma categoria com este nome.");

        Categoria? categoria = repositorioCategoria.SelecionarPorId(dto.Id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        Categoria categoriaAtualizada = new Categoria(dto.Nome, dto.Cor);

        bool conseguiuEditar = repositorioCategoria.Editar(dto.Id, categoriaAtualizada);

        return Result.Ok();
    }

    public List<ListarCategoriaDto> SelecionarTodos()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        return categorias
            .Select(c => new ListarCategoriaDto(c.Id, c.Nome, c.Cor))
            .ToList();
    }

    public Result<DetalhesCategoriaDto> SelecionarPorId(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        return Result.Ok(
            new DetalhesCategoriaDto(categoria.Id, categoria.Nome, categoria.Cor)
        );
    }

    public Result Excluir(string id)
    {
        Categoria? categoria = repositorioCategoria.SelecionarPorId(id);

        if (categoria == null)
            return Result.Fail("Categoria não encontrada.");

        List<Produto> produtos = repositorioProduto.SelecionarTodos();

         foreach (Produto p in produtos)
        {
            if (string.Equals(p.Categoria.Id, id))
                return Result.Fail("Esta categoria não pode ser excluída pois está relacionada a um produto.");
        }

        repositorioCategoria.Excluir(id);

        return Result.Ok();
    }

    private bool ExisteCategoriaComNome(string nome, string? idIgnorado = null)
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        foreach (Categoria c in categorias)
        {
            if (c.Id != idIgnorado && string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase))
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
