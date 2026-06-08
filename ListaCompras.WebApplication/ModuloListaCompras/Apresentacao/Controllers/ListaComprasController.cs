using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ListaCompras.WebApplication.ModuloListaCompras.Servicos;
using ListaCompras.WebApplication.ModuloListaCompras.Apresentacao;
using ListaCompras.WebApplication.ModuloListaCompras.Aplicacao;

namespace ListaCompras.WebApplication.Apresentacao;

public class ListaComprasController : Controller
{
    private readonly IMapper _mapeador;
    private readonly ListaDeComprasService _servico;

    public ListaComprasController(IMapper mapeador, ListaDeComprasService servico)
    {
        _mapeador = mapeador;
        _servico = servico;
    }

    public IActionResult Listar()
    {
        var dtos = _servico.SelecionarTodos();
        var vms = _mapeador.Map<List<ListaComprasViewModel>>(dtos);
        return View(vms);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(ListaComprasFormViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var dto = _mapeador.Map<CadastrarListaComprasDto>(vm);
        _servico.Criar(dto);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public IActionResult Excluir(string id)
    {
        var dto = _servico.SelecionarPorId(id);
        if (dto == null) return RedirectToAction(nameof(Listar));

        var vm = _mapeador.Map<ListaComprasViewModel>(dto);
        return View(vm);
    }

    [HttpPost]
    public IActionResult Excluir(string id, bool confirmacao)
    {
        _servico.Excluir(id);
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
public IActionResult Editar(string id)
    {
        var lista = _servico.SelecionarPorId(id);
        if (lista == null) return NotFound();
        
        // Agora passamos o status que veio do serviço
        var dto = new EditarListaComprasDto(lista.Id, lista.Nome, lista.Status);
        return View(dto);
    }
    

    [HttpPost]
    public IActionResult Editar(string id, EditarListaComprasDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            _servico.Editar(id, dto);
            // Corrigido para Listar, pois seu método principal chama Listar, não Index
            return RedirectToAction(nameof(Listar));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }
}