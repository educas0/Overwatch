using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Overwatch.Models;
using Overwatch.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch.Controllers
{
    public class HeroeController : Controller
    {
        #region "Config y Constructor"

        private IDBAccess __servicioSQLServer;

        public HeroeController(IDBAccess servicioInyectado)
        {
            this.__servicioSQLServer = servicioInyectado;
        }

        #endregion

        /// <summary>
        /// carga la vista del registro de un heroe
        /// </summary>
        /// <returns>Vista Registro heroe</returns>
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registro(Heroe NuevoHeroe)
        {
            //se inserta el heroe
            bool Resultado = __servicioSQLServer.RegistrarHeroe(NuevoHeroe);

            if (Resultado)
            {
                return View("RegistroOK");
            }
            else
            {
                ErrorViewModel _ErrorModel = new ErrorViewModel();
                return View("Error",_ErrorModel);
            }
        }

    }
}
