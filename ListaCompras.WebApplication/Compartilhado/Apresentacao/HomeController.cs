using Microsoft.AspNetCore.Mvc;

namespace ListaCompras.WebApplication.Compartilhado.Apresentacao
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
