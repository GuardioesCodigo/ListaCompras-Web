using ListaCompras.WebApplication.Compartilhado;

namespace ListaCompras.WebApplication.ModuloCategoria.Dominio;

public class Categoria : EntidadeBase<Categoria>
{
    public string Nome { get; set; } = string.Empty;
    public CorCategoria Cor { get; set; } = CorCategoria.Nenhuma;

    public Categoria() { }

    public Categoria(string nome, CorCategoria cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
            
        else if (Nome.Length < 2 || Nome.Length > 50)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 50 caracteres.");

        if (Cor == CorCategoria.Nenhuma)
            erros.Add("O campo \"Cor\" deve ser selecionado.");

        else if (!Enum.IsDefined(Cor))
            erros.Add("O valor informado para \"Cor\" é inválido.");

        return erros;
    }

    public override void AtualizarDados(Categoria entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Cor = entidadeAtualizada.Cor;
    }
}
