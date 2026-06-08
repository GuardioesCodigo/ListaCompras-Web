using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras.Servicos;

public class ListaDeComprasService
{
    private readonly IListaDeComprasRepository _listaRepository;
    private readonly ContextoJson _contexto; // Adicionamos o contexto aqui

    public ListaDeComprasService(IListaDeComprasRepository listaRepository, ContextoJson contexto)
    {
        _listaRepository = listaRepository;
        _contexto = contexto;
        
        // Carregamos os dados aqui, uma única vez, na inicialização do serviço
        _contexto.Carregar();
    }

    public void Criar(CadastrarListaComprasDto dto)
    {
        var lista = new ListaDeCompras(dto.Nome);
        _listaRepository.Cadastrar(lista);
        
        // Garantimos que a alteração seja persistida no arquivo
        _contexto.Salvar();
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
        
        // Persistimos a exclusão
        _contexto.Salvar();
    }

    public void Editar(string id, EditarListaComprasDto dto)
{
    var listaExistente = _listaRepository.SelecionarPorId(id);
    if (listaExistente == null) throw new Exception("Lista não encontrada.");

    // Atualiza os dados usando o método que você já tem na Entidade
    listaExistente.Nome = dto.Nome;
    // Se precisar atualizar status:
    // listaExistente.Status = dto.Status;

    _listaRepository.Editar(id, listaExistente);
    
    // Salva no arquivo JSON
    _contexto.Salvar();
}
}