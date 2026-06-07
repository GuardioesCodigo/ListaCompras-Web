using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras;

public class RepositorioListaComprasEmArquivo : RepositorioBaseEmArquivo<ListaDeCompras>, IListaDeComprasRepository
{
    public RepositorioListaComprasEmArquivo(ContextoJson contexto) : base(contexto) 
    { 
    }

    protected override List<ListaDeCompras> ObterListaDoContexto()
    {
        return contexto.ListaCompras;
    }

    // Usamos 'new' para ocultar a implementação da base, 
    // já que a base não permite 'override'.
    
    public new List<ListaDeCompras> SelecionarTodos() 
    {
        return base.SelecionarTodos();
    }

    public new ListaDeCompras? SelecionarPorId(string id) 
    {
        return base.SelecionarPorId(id);
    }

    public new bool Editar(string id, ListaDeCompras entidade) 
    {
        return base.Editar(id, entidade);
    }

    public new bool Excluir(string id) 
    {
        return base.Excluir(id);
    }

    public new List<ListaDeCompras> Filtrar(Predicate<ListaDeCompras> filtro) 
    {
        return base.Filtrar(filtro);
    }
}