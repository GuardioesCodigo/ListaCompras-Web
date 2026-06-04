using ListaCompras.WebApplication.Compartilhado.Dominio;

namespace ListaCompras.WebApplication.ModuloCategoria.Dominio;

public class ServicoCategoria
{
    private readonly IRepositorio<Categoria> repositorio;

    public ServicoCategoria(IRepositorio<Categoria> repositorio)
    {
        this.repositorio = repositorio;
    }

    public void Cadastrar(Categoria categoria)
    {
        var existe = repositorio
            .Filtrar(c => c.Nome == categoria.Nome)
            .Any();

        if (existe)
            throw new Exception("Já existe uma categoria com esse nome.");

        repositorio.Cadastrar(categoria);
    }
}
