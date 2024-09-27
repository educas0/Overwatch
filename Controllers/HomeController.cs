using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Overwatch.Models;
using Overwatch.Models.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch.Controllers
{
    public class HomeController : Controller
    {
        private IDBAccess __servicioSQLServer;
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor _httpcontext;

        public HomeController(ILogger<HomeController> logger,
                              IDBAccess _ServicioSql,
                              IHttpContextAccessor httpContext)
        {
            __servicioSQLServer = _ServicioSql;
            _logger = logger;
            _httpcontext = httpContext;
        }

        public IActionResult Index()        
        {
            return View();
        }


        public IActionResult SeleccionarHeroeSession()
        {
            List<Heroe> _Heroes = this.__servicioSQLServer.RecuperarListaHeroes();
            ViewData["listaHeroes"] = _Heroes;

            return View("HeroeSession");
        }

        [HttpPost]
        public IActionResult IniciarSessionHeroe(Heroe _h)
        {

            Heroe _Heroe = this.__servicioSQLServer.RecuperarListaHeroes(_h.IdHeroe);

            this._httpcontext.HttpContext.Session.SetString("heroe", JsonConvert.SerializeObject(_Heroe));

            return View("Index");

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
