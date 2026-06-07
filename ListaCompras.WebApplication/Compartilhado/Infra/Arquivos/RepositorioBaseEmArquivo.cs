using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;

namespace ListaCompras.WebApplication.Compartilhado.Arquivos;

public abstract class RepositorioBaseEmArquivo<T> where T : EntidadeBase<T>
{
    protected ContextoJson contexto;

    public RepositorioBaseEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
    }

    protected abstract List<T> ObterListaDoContexto();

    // --- MÉTODOS QUE FALTAVAM NA BASE ---

    public virtual List<T> SelecionarTodos()
    {
        return ObterListaDoContexto();
    }

    public virtual T? SelecionarPorId(string idSelecionado)
    {
        return ObterListaDoContexto().FirstOrDefault(r => r.Id == idSelecionado);
    }

    public virtual bool Editar(string idSelecionado, T entidadeAtualizada)
    {
        T? registro = SelecionarPorId(idSelecionado);
        if (registro == null) return false;

        registro.AtualizarDados(entidadeAtualizada);
        contexto.Salvar();
        return true;
    }

    public virtual bool Excluir(string idSelecionado)
    {
        T? registro = SelecionarPorId(idSelecionado);
        if (registro == null) return false;

        bool conseguiu = ObterListaDoContexto().Remove(registro);
        if (conseguiu) contexto.Salvar();
        return conseguiu;
    }

    public virtual List<T> Filtrar(Predicate<T> filtro)
    {
        return ObterListaDoContexto().FindAll(filtro);
    }

    public virtual void Cadastrar(T entidade)
    {
        ObterListaDoContexto().Add(entidade);
        contexto.Salvar();
    }
}