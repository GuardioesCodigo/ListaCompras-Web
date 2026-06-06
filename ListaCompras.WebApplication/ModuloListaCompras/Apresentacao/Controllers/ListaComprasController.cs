using Microsoft.AspNetCore.Mvc;
using ListaDeCompras.WebApplication.ModuloListaCompras.Servicos; 
using ListaDeCompras.WebApplication.ModuloListaCompras; 

namespace ListaDeCompras.WebApplication.ModuloListaCompras.Apresentacao.Controllers;

public class ListaComprasController : Controller
{
    private readonly ListaDeComprasService _listaService;

    public ListaComprasController(ListaDeComprasService listaService)
    {
        _listaService = listaService;
    }

    // LISTAR: Busca os dados reais gravados no repositório
    public IActionResult Listar()
    {
        var listas = _listaService.ObterTodas();

        // Caminho explícito para garantir que o .NET ache a View na estrutura modular
        return View("~/ModuloListaCompras/Apresentacao/Views/Listar.cshtml", listas);
    }

    // CADASTRAR [GET]: Exibe o formulário de cadastro
    public IActionResult Cadastrar() 
    {
        return View("~/ModuloListaCompras/Apresentacao/Views/Cadastrar.cshtml");
    }

    // CADASTRAR [POST]: Recebe os dados do formulário e grava de verdade
    [HttpPost]
    public IActionResult Cadastrar(ListaDeComprasFormViewModel vm)
    {
        if (!ModelState.IsValid) 
            return View("~/ModuloListaCompras/Apresentacao/Views/Cadastrar.cshtml", vm);

        // 1. Executa a criação através do Service
        _listaService.Criar(vm.Nome);
        
        TempData["MensagemSucesso"] = "Lista cadastrada com sucesso!";

        // 2. Redireciona para a listagem atualizada
        return RedirectToAction("Listar"); 
    }

    // EXCLUIR [POST]: Remove uma lista caso ela não possua itens
    [HttpPost]
    public IActionResult Excluir(string id)
    {
        try
        {
            _listaService.Excluir(id);
            TempData["MensagemSucesso"] = "Lista excluída com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex.Message; 
        }
        return RedirectToAction("Listar"); 
    }
}