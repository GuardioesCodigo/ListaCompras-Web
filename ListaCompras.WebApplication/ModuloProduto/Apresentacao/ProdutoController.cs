using ListaCompras.WebApplication.ModuloCategoria.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.ModuloProduto.Apresentacao
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            return View();
        }

    }
}
