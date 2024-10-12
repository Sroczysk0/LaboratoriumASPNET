using LaboratoriumASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaboratoriumASPNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calculator(string op, double? a, double? b)
        {
            if (a is null || b is null)
            {
                ViewBag.ErrorMessage = "Niepoprawny format liczby w parametrze a lub b";
                return View("CustomError");
            }
            //dodaj obsluge bledu dla op jesli jest inny od add, syb, mul, div
            if (op != "add" && op != "sub" && op != "mul" && op != "div")
            {
                ViewBag.ErrorMessage2 = "Niepoprawy format op";
                return View("CustomError02");

            }
            ViewBag.op = op;
            ViewBag.a = a;
            ViewBag.b = b;
            switch (op)
            {
                case "add":
                    ViewBag.result = a + b;
                    ViewBag.op = "+";
                    break;
                case "sub":
                    ViewBag.result = a - b;
                    ViewBag.op = "-";
                    break;
                case "mul":
                    ViewBag.result = a * b;
                    ViewBag.op = "*";
                    break;
                case "div":
                    ViewBag.result = a / b;
                    ViewBag.op = "/";
                    break;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
