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
        // Precisamos buscar para mostrar na tela de confirmação
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
}