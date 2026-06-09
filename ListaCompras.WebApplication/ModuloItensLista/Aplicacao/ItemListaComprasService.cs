using ListaCompras.WebApplication.ModuloListaCompras.Aplicacao; // Ajuste para o namespace correto dos seus DTOs
using ListaCompras.WebApplication.ModuloListaCompras.Dominio; 
using ListaCompras.WebApplication.ModuloProduto.Dominio; 
using ListaCompras.WebApplication.Compartilhado.Dominio;

namespace ListaCompras.WebApplication.ModuloItensLista.Servicos;

public class ItemListaComprasService
{
    private readonly IListaDeComprasRepository _listaRepository;
    private readonly IRepositorio<Produto> _produtoRepository;

    public ItemListaComprasService(IListaDeComprasRepository listaRepository, IRepositorio<Produto> produtoRepository)
    {
        _listaRepository = listaRepository;
        _produtoRepository = produtoRepository;
    }

    public List<ItemListaComprasDto> ObterItensDaLista(string listaId)
    {
        var lista = _listaRepository.SelecionarPorId(listaId);
        if (lista == null) return new List<ItemListaComprasDto>();

        // Refatorado para usar o construtor do record
        return lista.Itens.Select(i => new ItemListaComprasDto(
            i.Id,
            i.ProdutoId,
            i.NomeProduto,
            i.CategoriaProduto,
            i.Quantidade,
            i.Preco
        )).ToList();
    }

    public void AdicionarItem(string listaId, string produtoId, int quantidade)
    {
        var lista = _listaRepository.SelecionarPorId(listaId);
        var produto = _produtoRepository.SelecionarPorId(produtoId);

        if (lista == null) throw new Exception("Lista de compras não existe.");
        if (produto == null) throw new Exception("Produto inválido.");

        lista.AdicionarItem(produto, quantidade, produto.PrecoAproximado);
        
        _listaRepository.Editar(lista.Id, lista);
    }

    public void RemoverItem(string listaId, string itemId)
    {
        var lista = _listaRepository.SelecionarPorId(listaId);
        if (lista == null) throw new Exception("Lista não encontrada.");

        lista.RemoverItem(itemId);
        
        _listaRepository.Editar(lista.Id, lista);
    }
}