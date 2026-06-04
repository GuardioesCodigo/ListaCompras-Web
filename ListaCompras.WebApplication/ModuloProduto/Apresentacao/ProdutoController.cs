using AutoMapper;
using FluentResults;
using ListaCompras.WebApplication.Compartilhado.Apresentacao.Extensions;
using ListaCompras.WebApplication.ModuloProduto.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao;
public class ProdutoController(ServicoProduto servicoProduto, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutoDto> dtos = servicoProduto.SelecionarTodos();

        List<ListarProdutosViewModel> listarVms = mapeador.Map<List<ListarProdutosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {

        
        CadastrarProdutoViewModel cadastrarVm = new CadastrarProdutoViewModel(
            string.Empty,
            null,
            string.Empty,
            0m
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarProdutoDto dto = mapeador.Map<CadastrarProdutoDto>(cadastrarVm);

        Result resultado = servicoProduto.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

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

        DetalhesProdutoDto dto =  resultado.Value;

        EditarProdutoViewModel editarVm = mapeador.Map<EditarProdutoViewModel>(dto);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);    

        EditarProdutoDto dto = mapeador.Map<EditarProdutoDto>(editarVm);

        Result resultado = servicoProduto.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }
            
        return RedirectToAction(nameof(Listar));
    }
}