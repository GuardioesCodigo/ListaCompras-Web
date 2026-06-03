using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

namespace ListaCompras.WebApplication.ModuloListaCompras;

public class RepositorioListaComprasEmArquivo : RepositorioBaseEmArquivo<ListaDeCompras>
{
    public RepositorioListaComprasEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ListaDeCompras> CarregarRegistros()
    {
        return contexto.ListaCompras;
    }
}
