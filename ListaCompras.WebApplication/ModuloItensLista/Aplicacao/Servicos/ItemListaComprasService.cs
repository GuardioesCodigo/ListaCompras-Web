using ListaCompras.Application.DTOs;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio; 
using ListaCompras.WebApplication.ModuloProduto.Dominio; 
using ListaCompras.WebApplication.Compartilhado.Dominio; // 👈 ADICIONADO: Para enxergar o IRepositorio genérico

namespace ListaCompras.WebApplication.ModuloItensLista.Servicos;

public class ItemListaComprasService
{
    private readonly IListaDeComprasRepository _listaRepository;
    private readonly IRepositorio<Produto> _produtoRepository; // 👈 ALTERADO: De IRepositorioProduto para IRepositorio<Produto>

    // 👈 ALTERADO: Construtor agora aceita o tipo genérico correto
    public ItemListaComprasService(IListaDeComprasRepository listaRepository, IRepositorio<Produto> produtoRepository)
    {
        _listaRepository = listaRepository;
        _produtoRepository = produtoRepository;
    }

    public List<ItemListaComprasDto> ObterItensDaLista(string listaId)
    {
        var lista = _listaRepository.SelecionarPorId(listaId);
        if (lista == null) return new List<ItemListaComprasDto>();

        return lista.Itens.Select(i => new ItemListaComprasDto
        {
            Id = i.Id,
            ProdutoId = i.Produto.Id,
            NomeProduto = i.Produto.Nome,
            CategoriaProduto = i.Produto.Categoria?.Nome ?? "Sem Categoria",
            Quantidade = i.Quantidade,
            PrecoUnitario = i.Preco
        }).ToList();
    }

    public void AdicionarItem(string listaId, string produtoId, int quantidade)
    {
        var lista = _listaRepository.SelecionarPorId(listaId);
        var produto = _produtoRepository.SelecionarPorId(produtoId); // 👈 Continua funcionando igual porque o genérico tem esse método!

        if (lista == null) throw new Exception("Lista de compras não existe.");
        if (produto == null) throw new Exception("Produto inválido.");

        lista.AdicionarItem(produto, quantidade);
        
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