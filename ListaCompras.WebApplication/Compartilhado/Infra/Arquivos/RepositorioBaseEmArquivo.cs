using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Dominio;

namespace ListaCompras.WebApplication.Compartilhado.Arquivos;

public abstract class RepositorioBaseEmArquivo<T> : IRepositorio<T> where T : EntidadeBase<T>
{
    protected ContextoJson contexto;

    public RepositorioBaseEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
    }

    // Agora o acesso é centralizado e direto no contexto
    protected abstract List<T> ObterListaDoContexto();

    public void Cadastrar(T entidade)
    {
        ObterListaDoContexto().Add(entidade);
        contexto.Salvar();
    }

    public virtual bool Editar(string id, T entidadeAtualizada)
    {
        var lista = ObterListaDoContexto();
        var entidade = lista.FirstOrDefault(r => r.Id == id);
        
        if (entidade == null) return false;

        // Atualiza a posição exata na lista do contexto
        int index = lista.IndexOf(entidade);
        lista[index] = entidadeAtualizada;
        
        contexto.Salvar();
        return true;
    }

    public virtual bool Excluir(string id)
    {
        var lista = ObterListaDoContexto();
        var entidade = lista.FirstOrDefault(r => r.Id == id);
        
        if (entidade == null) return false;

        lista.Remove(entidade);
        contexto.Salvar();
        return true;
    }

    public T? SelecionarPorId(string id) => ObterListaDoContexto().FirstOrDefault(r => r.Id == id);

    public List<T> SelecionarTodos() => ObterListaDoContexto();

    public List<T> Filtrar(Predicate<T> filtro) => ObterListaDoContexto().FindAll(filtro);
}