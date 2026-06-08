using ListaCompras.WebApplication.Compartilhado;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;

namespace ListaCompras.WebApplication.ModuloProduto.Dominio;

public class Produto : EntidadeBase<Produto>
{
    public string Nome { get; set; } = string.Empty;
    public UnidadeMedida UnidadeMedida { get; set; } =  UnidadeMedida.unidade;
    public decimal PrecoAproximado { get; set; } = 0m;
    public Categoria Categoria { get; set; } = null!;

    public Produto()
    {
    }

    public Produto(
        string nome,
        UnidadeMedida unidadeMedida,
        decimal precoAproximado,
        Categoria categoria
    )
    {
        Nome = nome;
        UnidadeMedida = unidadeMedida;
        PrecoAproximado = precoAproximado;
        Categoria = categoria;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (Categoria == null)
            erros.Add("O campo \"Categoria\" deve ser preenchido.");

        if (!Enum.IsDefined(typeof(UnidadeMedida), UnidadeMedida))
            erros.Add("O campo \"Unidade de Medida\" deve ser preenchido.");

        if (PrecoAproximado == 0)
            erros.Add("O campo \"Preço Aproximado\" deve ser preenchido.");

        return erros;
    }

    public override void AtualizarDados(Produto entidadeAtualizada)
    {
        Produto produtoAtualizado = (Produto)entidadeAtualizada;

        Nome = produtoAtualizado.Nome;
        UnidadeMedida = produtoAtualizado.UnidadeMedida;
        PrecoAproximado = produtoAtualizado.PrecoAproximado;
        Categoria = produtoAtualizado.Categoria;
    }

}
