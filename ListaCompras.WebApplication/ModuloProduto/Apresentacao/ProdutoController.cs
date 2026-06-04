using AutoMapper;
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

}