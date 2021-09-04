using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace WebAPI_MVC.Models
{
    public class Api
    {
        public static List<Provincia> ConsultaApi()
        {
            var url = $"https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            ProvArg provincias = null;

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                        {
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                provincias = JsonSerializer.Deserialize<ProvArg>(responseBody);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception("Error de conexion con la Api");
            }

            return provincias.provincias;

        }

        public class Parametros
        {
            public List<string> campos { get; set; }
        }

        public class Provincia
        {
            public string id { get; set; }

            public string nombre { get; set; }
        }

        public class ProvArg
        {
            public int cantidad { get; set; }

            public int inicio { get; set; }

            public Parametros parametros { get; set; }

            public List<Provincia> provincias { get; set; }

            public int total { get; set; }
        }
    }
}
