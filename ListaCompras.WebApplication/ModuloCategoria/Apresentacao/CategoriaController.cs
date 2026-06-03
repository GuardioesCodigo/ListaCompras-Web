using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaController: Controller
{
    private readonly IRepositorioCategoria repositorioCategoria;

    [HttpGet]
    public ActionResult Listar()
    {
        List<Categoria> categorias = repositorioCategoria.SelecionarTodos();

        List<ListarCategoriasViewModel> listarVms = new List<ListarCategoriasViewModel>();

        foreach (Categoria c in categorias)
        {
            ListarCategoriasViewModel viewModel = new ListarCategoriasViewModel(
                c.Id,
                c.Nome,
                c.Cor
            );

            listarVms.Add(viewModel);
        }

        return View(listarVms);
    }
}