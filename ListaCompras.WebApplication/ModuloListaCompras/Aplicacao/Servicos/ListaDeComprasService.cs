using ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras.Servicos;

public class ListaDeComprasService
{
    private readonly IListaDeComprasRepository _listaRepository;

    public ListaDeComprasService(IListaDeComprasRepository listaRepository)
    {
        _listaRepository = listaRepository;
    }

    // Criar agora recebe o Record de cadastro
    public void Criar(CadastrarListaComprasDto dto)
    {
        var lista = new ListaDeCompras(dto.Nome);
        _listaRepository.Cadastrar(lista);
    }

    public List<ListarListaComprasDto> SelecionarTodos()
    {
        return _listaRepository.SelecionarTodos().Select(l => new ListarListaComprasDto(
            l.Id,
            l.Nome,
            l.DataCriacao,
            l.Status,
            l.TotalItens,
            l.TotalGasto
        )).ToList();
    }

    public DetalhesListaComprasDto? SelecionarPorId(string id)
    {
        var l = _listaRepository.SelecionarPorId(id);
        if (l == null) return null;

        return new DetalhesListaComprasDto(
            l.Id,
            l.Nome,
            l.DataCriacao,
            l.Status,
            l.TotalItens,
            l.TotalGasto
        );
    }

    public void Excluir(string id)
    {
        var lista = _listaRepository.SelecionarPorId(id);
        if (lista == null) throw new Exception("Lista não encontrada.");
        
        if (lista.Itens.Count > 0)
            throw new Exception("Regra de Negócio: Não é possível excluir uma lista que possui itens vinculados.");

        _listaRepository.Excluir(id);
    }
}