using ListaCompras.WebApplication.Compartilhado.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Infra;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>, IRepositorio<Produto>
{
    public RepositorioProdutoEmArquivo(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<Produto> CarregarRegistros()
    {
        return contexto.Produtos;
    }
}
