using System.Security.Cryptography;

namespace ListaCompras.WebApplication.Compartilhado;

public abstract class EntidadeBase <T>
{
    public Guid Id { get; set; }= Guid.CreateVersion7();
    public abstract List<string> Validar();
    public abstract void AtualizarDados(T entidadeAtualizada);
}

