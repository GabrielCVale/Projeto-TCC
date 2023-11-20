using Microsoft.AspNetCore.Mvc;
using Projeto_TCC.Data;
using Projeto_TCC.Models;
using Tcc.Data;
using System.Diagnostics;

namespace Projeto_TCC.Controllers
{
    public class HomeController : Controller
    {
        private readonly MySqlConnectionHelper _mySqlConnectionHelper;

        public HomeController(MySqlConnectionHelper mySqlConnectionHelper)
        {
            _mySqlConnectionHelper = mySqlConnectionHelper;
        }
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Cadastro()
        {
            return View("Cadastro");
        }

        public ActionResult Viagens()
        {
            var viagens = _mySqlConnectionHelper.ObterViagens();
            return View("Viagens",viagens);
        }

        public ActionResult Card(int id)
        {
            try
            {
                var detalhes = _mySqlConnectionHelper.ObterDetalhesViagem(id);

                if (detalhes != null && detalhes.Any())
                {
                    return PartialView("_Detalhes", detalhes);
                }
                else
                {
                    return PartialView("_Detalhes", null); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPost]
        [Route("Home/Validar")]
        public ActionResult Validar(string email, string senha)
        {

            if (email == "a" && senha == "a")
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