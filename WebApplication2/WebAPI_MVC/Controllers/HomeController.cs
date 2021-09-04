using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using NLog;
using WebAPI_MVC.Models;
using static WebAPI_MVC.Models.Api;

namespace WebAPI_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string Punto3()
        {
            try
            {
                string dato = "";
                List<Provincia> ListadoDeProvincias = ConsultaApi();
                foreach (var prov in ListadoDeProvincias)
                {
                    dato += $"id: {prov.id}. Provincia: {prov.nombre} \n";
                }
                return dato;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return "Error: " + e.Message.ToString();

            }
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
