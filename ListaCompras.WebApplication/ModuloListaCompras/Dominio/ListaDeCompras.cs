using ListaCompras.WebApplication.Compartilhado;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio;

namespace ListaCompras.WebApplication.ModuloListaCompras;

public class ListaDeCompras : EntidadeBase<ListaDeCompras>
{
    public string Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    public StatusListaCompras Status { get; set; }
    public List<ItemListaCompras> Itens { get; set; } = new List<ItemListaCompras>();

    // REQUISITO: Exibir o total de itens da lista
    public int TotalItens => Itens.Count;

    // REQUISITO: Valor total calculado automaticamente (Preço estimado × Quantidade)
    public decimal TotalGasto
    {
        get
        {
            decimal totalGasto = 0;

            foreach (ItemListaCompras item in Itens)
            {
                // Multiplica o preço unitário do produto pela quantidade do item
                totalGasto += (item.Preco * item.Quantidade);
            }

            return totalGasto;
        }
    }

    public ListaDeCompras()
    {
    }

    public ListaDeCompras(string nome)
    {
        Nome = nome;
        DataCriacao = DateTime.Now; // REQUISITO: Data de criação automática
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

    // REQUISITO: Não pode adicionar o mesmo produto duas vezes na mesma lista
    public void AdicionarItem(Produto produto, int quantidade)
    {
        // Verifica se já existe algum item com o ID deste produto
        foreach (var itemExistente in Itens)
        {
            if (itemExistente.Produto.Id == produto.Id)
            {
                throw new Exception("Este produto já foi adicionado a esta lista de compras.");
            }
        }

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

        // REQUISITO: Nome obrigatório entre 3 e 100 caracteres
        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        return erros;
    }

    public override void AtualizarDados(ListaDeCompras entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Status = entidadeAtualizada.Status; // Permite atualizar o status se mudar para Concluída
    }
}