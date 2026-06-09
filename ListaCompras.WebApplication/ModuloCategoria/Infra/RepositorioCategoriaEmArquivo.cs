using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.ConsoleApp.ModuloCategoria.Infra;

public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
{
    public RepositorioCategoriaEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    // Apenas este método deve existir aqui
    protected override List<Categoria> ObterListaDoContexto()
    {
        return contexto.Categorias;
    }
}