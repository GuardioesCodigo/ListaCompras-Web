using System.ComponentModel.DataAnnotations;

public class ListaDeComprasViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O nome da lista é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; }

    public string Status { get; set; } // Aberta / Concluida
    public DateTime DataCriacao { get; set; }
}