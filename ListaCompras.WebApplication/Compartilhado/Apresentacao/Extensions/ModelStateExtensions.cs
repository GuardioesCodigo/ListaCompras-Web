using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ListaCompras.WebApplication.Compartilhado.Apresentacao.Extensions; 

public static class ModelStateExtensions
{
    public static void AddModelError(this ModelStateDictionary modelState, ResultBase result)
    {
        foreach (IError erro in result.Errors)
        {
            erro.Metadata.TryGetValue("Campo", out var campoObj);

            string campo = campoObj?.ToString() ?? string.Empty;

            modelState.AddModelError(campo, erro.Message);
        }
    }
}
