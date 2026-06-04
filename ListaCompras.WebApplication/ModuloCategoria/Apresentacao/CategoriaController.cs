using FluentResults;
using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.ModuloCategoria.Apresentacao;

public class CategoriaController: Controller
{
    private readonly IRepositorioCategoria repositorioCategoria;
    private readonly ServicoCategoria servicoCategoria;

    public CategoriaController(ServicoCategoria servicoCategoria)
    {
        ContextoJson contexto = new ContextoJson();
        contexto.Carregar();

        repositorioCategoria = new RepositorioCategoriaEmArquivo(contexto);
        this.servicoCategoria = servicoCategoria;
    }

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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCategoriasViewModel cadastrarVm = new CadastrarCategoriasViewModel(
            string.Empty,
            CorCategoria.Nenhuma
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCategoriasViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Result resultado = servicoCategoria.Cadastrar(cadastrarVm);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo =
                    erro.Metadata["Campo"] is string
                        ? erro.Metadata["Campo"].ToString()!
                        : string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}