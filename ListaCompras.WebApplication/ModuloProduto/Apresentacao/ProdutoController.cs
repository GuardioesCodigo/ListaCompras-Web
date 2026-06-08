using AutoMapper;
using FluentResults;
using ListaCompras.ConsoleApp.ModuloCategoria.Infra;
using ListaCompras.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaCompras.WebApplication.Compartilhado.Dominio;
using ListaCompras.WebApplication.Compartilhado.Infra.Arquivos;
using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;
using ListaCompras.WebApplication.ModuloCategoria.Apresentacao;
using ListaCompras.WebApplication.ModuloCategoria.Dominio;
using ListaCompras.WebApplication.ModuloProduto.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao;

public class ProdutoController(ServicoProduto servicoProduto, ServicoCategoria servicoCategoria, IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar(string categoriaNome)
    {
        List<ListarProdutoDto> dtos = servicoProduto.SelecionarTodos();

        if (!string.IsNullOrEmpty(categoriaNome))
        {
            dtos = dtos
                .Where(p => p.CategoriaNome == categoriaNome)
                .ToList();
        }

        ViewBag.Categorias = servicoCategoria.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = mapeador.Map<List<ListarProdutosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Categoria = CarregarCategorias();
        
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categoria = CarregarCategorias();
            return View(cadastrarVm);
        }

        CadastrarProdutoDto dto = mapeador.Map<CadastrarProdutoDto>(cadastrarVm);

        Result resultado = servicoProduto.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ViewBag.Categoria = CarregarCategorias();

            ModelState.Remove(nameof(CadastrarProdutoViewModel.CategoriaId));

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(string id)
    {
        Result<DetalhesProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesProdutoDto produto = resultado.Value;

        EditarProdutoViewModel editarVm = mapeador.Map<EditarProdutoViewModel>(produto);

        ViewBag.Categoria = CarregarCategorias();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categoria = CarregarCategorias();
            return View(editarVm);
        }  

        EditarProdutoDto dto = mapeador.Map<EditarProdutoDto>(editarVm);

        Result resultado = servicoProduto.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            ViewBag.Categoria = CarregarCategorias();

            ModelState.Remove(nameof(CadastrarProdutoViewModel.CategoriaId));

            return View(editarVm);
        }
            
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(string id)
    {
        Result<DetalhesProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesProdutoDto dto = resultado.Value;

        ExcluirProdutosViewModel excluirVm =  mapeador.Map<ExcluirProdutosViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirProdutosViewModel excluirVm)
    {
        Result resultado = servicoProduto.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    private List<SelectListItem> CarregarCategorias()
    {
        List<ListarCategoriaDto> categorias = servicoCategoria.SelecionarTodos();

        return categorias
            .Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Nome
            })
            .ToList();
    }
}