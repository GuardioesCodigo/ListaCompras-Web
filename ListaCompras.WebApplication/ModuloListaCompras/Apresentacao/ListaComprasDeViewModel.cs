using System.ComponentModel.DataAnnotations;

namespace ListaCompras.WebApplication.ModuloListaCompras.Apresentacao;

public class ListaComprasViewModel
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    public string DataCriacaoFormatada { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int TotalItens { get; set; }
    public string TotalGastoFormatado { get; set; } = string.Empty;
}