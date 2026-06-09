using Microsoft.AspNetCore.Mvc;
using ListaCompras.WebApplication.ModuloItensLista.Servicos; 
using ListaCompras.WebApplication.ModuloListaCompras.Servicos; 
using ListaCompras.WebApplication.ModuloProduto.Dominio; 
using ListaCompras.WebApplication.ModuloItensLista; // Garante o import do ItemListaFormViewModel se necessário

namespace ListaCompras.WebApplication.ModuloItensLista.Apresentacao.Controllers;

[Route("ItensLista/[action]")]
public class ItensListaController : Controller
{
    private readonly ItemListaComprasService _itemService;
    private readonly ListaDeComprasService _listaService;
    private readonly IRepositorioProduto _produtoRepo; 

    public ItensListaController(ItemListaComprasService itemService, ListaDeComprasService listaService, IRepositorioProduto produtoRepo)
    {
        _itemService = itemService;
        _listaService = listaService;
        _produtoRepo = produtoRepo;
    }

    public IActionResult Gerenciar(string listaId)
    {
        var listaDto = _listaService.SelecionarPorId(listaId);
        var debugListas = _listaService.SelecionarTodos(); 
    
    
        if (listaDto == null) 
        {
        
            return NotFound(); 
        }


        ViewBag.Lista = listaDto;
        ViewBag.Itens = _itemService.ObterItensDaLista(listaId);
        ViewBag.ProdutosDisponiveis = _produtoRepo.SelecionarTodos();

        var model = new ItemListaFormViewModel { ListaComprasId = listaId };
        
        // CORREÇÃO: Apontando o caminho físico exato da sua View customizada
        return View("Gerenciar", model);
    }

    [HttpPost]
    public IActionResult Adicionar(ItemListaFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            TempData["MensagemErro"] = "Verifique os dados informados.";
            return RedirectToAction("Gerenciar", new { listaId = vm.ListaComprasId });
        }

        try
        {
            _itemService.AdicionarItem(vm.ListaComprasId, vm.ProdutoId, vm.Quantidade);
            TempData["MensagemSucesso"] = "Item adicionado com sucesso!";
        }
        catch (Exception ex)
        {
            TempData["MensagemErro"] = ex.Message;
        }

        return RedirectToAction("Gerenciar", new { listaId = vm.ListaComprasId });
    }

    [HttpPost]
    public IActionResult Remover(string listaId, string itemId)
    {
        _itemService.RemoverItem(listaId, itemId);
        TempData["MensagemSucesso"] = "Item removido com sucesso!";
        return RedirectToAction("Gerenciar", new { listaId = listaId });
    }
}