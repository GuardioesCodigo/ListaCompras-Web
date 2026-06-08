using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras;

// Adicione o IListaDeComprasRepository após a classe base
public class RepositorioListaComprasEmArquivo : 
    RepositorioBaseEmArquivo<ListaDeCompras>, IListaDeComprasRepository
{
    public RepositorioListaComprasEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ListaDeCompras> CarregarRegistros()
    {
        return contexto.ListaCompras;
    }
}