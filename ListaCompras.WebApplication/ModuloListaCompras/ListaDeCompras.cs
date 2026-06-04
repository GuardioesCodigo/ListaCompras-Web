using ListaCompras.WebApplication.Compartilhado;
using ListaCompras.WebApplication.ModuloListaCompras;
using ListaCompras.WebApplication.ModuloProduto;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras;

public class ListaDeCompras : EntidadeBase<ListaDeCompras>
{
    public string Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    public StatusListaCompras Status { get; set; }
    public List<ItemListaCompras> Itens { get; set; } = new List<ItemListaCompras>();
    public decimal TotalGasto
    {
        get
        {
            decimal totalGasto = 0;

            foreach (ItemListaCompras item in Itens)
                totalGasto += item.Preco;

            return totalGasto;
        }
    }

    public ListaDeCompras()
    {
    }

    public ListaDeCompras(string nome)
    {
        Nome = nome;
        DataCriacao = DateTime.Now;

        Abrir();
    }

    public void Abrir()
    {
        Status = StatusListaCompras.Aberta;
    }

    public void Concluir()
    {
        Status = StatusListaCompras.Concluida;
    }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        ItemListaCompras item = new ItemListaCompras(produto, quantidade);

        Itens.Add(item);
    }

    public bool RemoverItem(string idItem)
    {
        foreach (ItemListaCompras item in Itens)
        {
            if (item.Id == idItem)
            {
                Itens.Remove(item);
                return true;
            }
        }

        return false;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        return erros;
    }

    public override void AtualizarDados(ListaDeCompras entidadeAtualizada)
    {
        ListaDeCompras listaAtualizada = (ListaDeCompras)entidadeAtualizada;

        Nome = listaAtualizada.Nome;
    }
}
