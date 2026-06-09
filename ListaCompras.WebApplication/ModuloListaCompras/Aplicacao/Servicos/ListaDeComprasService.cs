using ListaCompras.WebApplication.Compartilhado.Dominio; // Para IRepositorio
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio; // Para Produto

namespace ListaCompras.WebApplication.ModuloListaCompras.Servicos;

public class ListaDeComprasService
{
    private readonly IListaDeComprasRepository _listaRepository;
    private readonly IRepositorio<Produto> _repositorioProduto; // Campo adicionado
    private readonly ContextoJson _contexto;

    // Adicionei IRepositorio<Produto> aqui para injetar a dependência
    public ListaDeComprasService(
        IListaDeComprasRepository listaRepository, 
        IRepositorio<Produto> repositorioProduto, // Adicionado
        ContextoJson contexto)
    {
        _listaRepository = listaRepository;
        _repositorioProduto = repositorioProduto; // Inicializado
        _contexto = contexto;
        
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

public void AdicionarItem(string listaId, string produtoId, int quantidade)
{
    // 1. Busca a lista e o produto
    var lista = _listaRepository.SelecionarPorId(listaId);
    if (lista == null) throw new Exception("Lista não encontrada.");

    var produto = _repositorioProduto.SelecionarPorId(produtoId); 
    if (produto == null) throw new Exception("Produto não encontrado.");

    lista.AdicionarItem(produto, quantidade, produto.PrecoAproximado);
    
    // 3. Persiste a alteração
    _listaRepository.Editar(listaId, lista);
    _contexto.Salvar();
}
}