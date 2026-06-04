using FluentResults;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.ModuloCategoria.Apresentacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloCategoria.Aplicacao;

public class ServicoCategoria
{
    private readonly IRepositorio<Categoria> repositorio;

    public ServicoCategoria(IRepositorio<Categoria> repositorio)
    {
        this.repositorio = repositorio;
    }

    public Result Cadastrar(CadastrarCategoriasViewModel vm)
    {
    var existe = repositorio
        .Filtrar(c => c.Nome == vm.Nome)
        .Any();

    if (existe)
    {
        return Result.Fail(new Error("Já existe uma categoria com esse nome")
            .WithMetadata("Campo", "Nome"));
    }

    var categoria = new Categoria(vm.Nome, vm.Cor);

    repositorio.Cadastrar(categoria);

    return Result.Ok();
    }
}
