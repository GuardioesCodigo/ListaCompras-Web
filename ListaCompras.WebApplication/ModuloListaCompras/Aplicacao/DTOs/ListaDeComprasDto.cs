namespace ListaCompras.Application.DTOs;

public class ListaDeComprasDto
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataCriacao { get; set; }
    public string Status { get; set; }
    public int TotalItens { get; set; }
    public decimal TotalGasto { get; set; }
}