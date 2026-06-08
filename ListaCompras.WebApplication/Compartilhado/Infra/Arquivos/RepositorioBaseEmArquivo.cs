using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.Compartilhado.Dominio;

namespace ListaCompras.WebApplication.Compartilhado.Arquivos;

public abstract class RepositorioBaseEmArquivo<T> : IRepositorio<T> where T : EntidadeBase<T>
{
    protected ContextoJson contexto;
    protected List<T> registros;

    public RepositorioBaseEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
        this.registros = CarregarRegistros();
    }

    protected abstract List<T> CarregarRegistros();

    public void Cadastrar(T entidade)
    {
        registros.Add(entidade);
        contexto.Salvar(); // Sugestão: Salvar aqui garante a persistência
    }

    public virtual bool Editar(string id, T entidadeAtualizada)
    {
        var entidade = SelecionarPorId(id);
        if (entidade == null) return false;

        var lista = CarregarRegistros();
        int index = lista.IndexOf(entidade);
        lista[index] = entidadeAtualizada;
        
        contexto.Salvar();
        return true;
    }

    public virtual bool Excluir(string id)
    {
        var entidade = SelecionarPorId(id);
        if (entidade == null) return false;

        var lista = CarregarRegistros();
        lista.Remove(entidade);
        
        contexto.Salvar();
        return true;
    }

    public T? SelecionarPorId(string idSelecionado)
    {
        return registros.FirstOrDefault(r => r.Id == idSelecionado);
    }

    public List<T> SelecionarTodos()
    {
        return registros;
    }

    public List<T> Filtrar(Predicate<T> filtro)
    {
        return registros.FindAll(filtro);
    }
}