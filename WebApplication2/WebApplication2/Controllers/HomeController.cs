using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;
using static WebAPI_MVC.Models.Api;

namespace WebApplication2.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public int Punto1(int numero)
        {
            return Convert.ToInt32(Math.Pow(numero, 2));
        }

        public string Punto2(int dividendo, int divisor)
        {
            try
            {
                if (divisor == 0)
                {
                    throw new DivideByZeroException();
                }
                return Convert.ToString(dividendo / divisor);
            }
            catch (DivideByZeroException ex2)
            {
                return "Se produjo un error de división por cero - " + ex2.Message.ToString();
            }
            catch (Exception ex1)
            {
                return "Se produjo un error desconocido - " + ex1.Message.ToString();
            }
        }
        public string Punto3()
        {
            try
            {
                string cadena = "";
                List<Provincia> ListadoDeProvincias = ConsultaApi();
                foreach (var prov in ListadoDeProvincias)
                {
                    cadena += $"id: {prov.id}. Provincia: {prov.nombre} \n";
                }
                return cadena;
            }
            catch (Exception ex1)
            {
                _logger.LogError(ex1.Message);
                return "Error: " + ex1.Message.ToString();

            }
        }
        public string Punto4(string kilometros, string litros)
        {
            float resultado, km, l;
            string mensaje;

            try
            {
                if (kilometros != null && litros != null)
                {
                    km = float.Parse(kilometros, System.Globalization.NumberStyles.Float);
                    l = float.Parse(litros, System.Globalization.NumberStyles.Float);

                    if (l != 0)
                    {
                        resultado = km / l;
                        mensaje = resultado.ToString();
                    }
                    else
                    {
                        mensaje = "No se puede dividir en 0";
                    }
                }
                else
                {
                    mensaje = "Ingrese los datos antes de calcular";
                }
            }
            catch (FormatException ex)
            {
                mensaje = "Los datos ingresados no son numeros o no están en el formato correcto";
                _logger.LogError(ex.ToString());
            }
            catch (Exception ex)
            {
                mensaje = "Valores ingresados fuera de los rangos esperados";
                _logger.LogError(ex.ToString());
            }

            return mensaje;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
