using Microsoft.AspNetCore.Mvc;
using Projeto_TCC.Models;
using System.Diagnostics;

namespace Projeto_TCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("Home/Validar")]
        public ActionResult Validar(string email, string senha)
        {
            if (email == "a" || senha == "a")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}