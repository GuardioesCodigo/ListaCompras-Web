using ListaCompras.Application.DTOs;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio; 

namespace ListaCompras.WebApplication.ModuloListaCompras.Servicos;

public class ListaDeComprasService
{
    private readonly IListaDeComprasRepository _listaRepository;

    public ListaDeComprasService(IListaDeComprasRepository listaRepository)
    {
        _listaRepository = listaRepository;
    }

    public void Criar(string nome)
    {
        var lista = new ListaDeCompras(nome);
        
        // AJUSTADO: De 'Inserir' para 'Cadastrar'
        _listaRepository.Cadastrar(lista);
    }

    public void Excluir(string id)
    {
        var lista = _listaRepository.SelecionarPorId(id);
        if (lista == null) throw new Exception("Lista não encontrada.");
        
        if (lista.Itens.Count > 0)
            throw new Exception("Regra de Negócio: Não é possível excluir uma lista que possui itens vinculados.");

        // AJUSTADO: Passando o parâmetro 'id' que o método genérico espera
        _listaRepository.Excluir(id);
    }

    public List<ListaDeComprasDto> ObterTodas()
    {
        return _listaRepository.SelecionarTodos().Select(l => new ListaDeComprasDto
        {
            Id = l.Id,
            Nome = l.Nome,
            DataCriacao = l.DataCriacao,
            Status = l.Status.ToString(),
            TotalItens = l.TotalItens,
            TotalGasto = l.TotalGasto
        }).ToList();
    }

    public ListaDeComprasDto ObterPorId(string id)
    {
        // AJUSTADO: Garante que está usando o nome id correto
        var l = _listaRepository.SelecionarPorId(id);
        if (l == null) return null;

        return new ListaDeComprasDto
        {
            Id = l.Id,
            Nome = l.Nome,
            DataCriacao = l.DataCriacao,
            Status = l.Status.ToString(),
            TotalItens = l.TotalItens,
            TotalGasto = l.TotalGasto
        };
    }
}