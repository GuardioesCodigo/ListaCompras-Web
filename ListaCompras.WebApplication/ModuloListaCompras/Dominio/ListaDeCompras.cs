using System.Text.Json.Serialization;
using ListaCompras.WebApplication.Compartilhado;
using ListaCompras.WebApplication.ModuloListaCompras.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Dominio;
using System.Linq;

namespace ListaCompras.WebApplication.ModuloListaCompras.Dominio;

public class ListaDeCompras : EntidadeBase<ListaDeCompras>
{
    // Corrigido: Inicializado com string.Empty para evitar avisos de null
    public string Nome { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public StatusListaCompras Status { get; set; }
    
    // O JsonIgnore aqui é crucial para evitar o erro de "Pilha" (Stack Overflow)
    // Se o ItemListaCompras também referencia esta Lista, o serializador trava.
    public List<ItemListaCompras> Itens { get; set; } = new List<ItemListaCompras>();

    public int TotalItens => Itens.Count;

    public decimal TotalGasto
    {
        get
        {
            // Uso de LINQ para deixar o código mais limpo e seguro
            return Itens.Sum(item => item.Preco * item.Quantidade);
        }
    }

    public ListaDeCompras()
    {
        // Construtor vazio necessário para o Serializador JSON
    }

    public ListaDeCompras(string nome)
    {
        Nome = nome;
        DataCriacao = DateTime.Now;
        Abrir();
    }

    public void Abrir() => Status = StatusListaCompras.Aberta;
    public void Concluir() => Status = StatusListaCompras.Concluida;

    public void AdicionarItem(Produto produto, int quantidade, decimal preco)
    {
        if (Itens.Any(i => i.Produto.Id == produto.Id))
            throw new Exception("Este produto já foi adicionado a esta lista de compras.");

        Itens.Add(new ItemListaCompras(produto, quantidade, preco));
    }

    public bool RemoverItem(string idItem)
    {
        var item = Itens.FirstOrDefault(i => i.Id == idItem);
        if (item != null)
        {
            return Itens.Remove(item);
        }
        return false;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();
        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");
        return erros;
    }

    public override void AtualizarDados(ListaDeCompras entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Status = entidadeAtualizada.Status;
    }
}