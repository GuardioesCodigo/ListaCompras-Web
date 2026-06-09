using System;
using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

namespace ListaCompras.WebApplication.ModuloItensLista.Infra;

public class RepositorioItensListaEmArquivo : RepositorioBaseEmArquivo<ItemListaCompras>, IRepositorio<ItemListaCompras>
{
    public RepositorioItensListaEmArquivo(ContextoJson contexto) : base(contexto)
    {
        
    }

    protected override List<ItemListaCompras> ObterListaDoContexto()
    {
        return contexto.ItemListaCompras;
    }
}
