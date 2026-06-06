using ListaDeCompras.WebApplication.Compartilhado.Arquivos;
using ListaDeCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaDeCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaDeCompras.WebApplication.ModuloListaCompras;

public class RepositorioListaComprasEmArquivo : RepositorioBaseEmArquivo<ListaDeCompras>, IListaDeComprasRepository
{
    public RepositorioListaComprasEmArquivo(ContextoJson contexto) : base(contexto)
    {
        // Força a lista interna da base a apontar exatamente para o arquivo físico gerenciado pelo contexto
        if (contexto != null)
        {
            base.registros = contexto.ListaCompras;
        }
    }

    protected override List<ListaDeCompras> CarregarRegistros()
    {
        if (contexto == null || contexto.ListaCompras == null)
        {
            return new List<ListaDeCompras>();
        }

        return contexto.ListaCompras; 
    }
}