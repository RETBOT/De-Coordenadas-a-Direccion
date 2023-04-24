using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
// By: RETBOT
namespace FullAddress
{
    class Program
    {
        /*
         * El código obtiene la dirección de una ubicación en Google Maps a 
         * partir un fragmento de direccióm
         */
        static async Task Main(string[] args)
        {
            //  Se definen los valores de la direccion
            // 701 W 2nd St, Muleshoe, TX 79347, EE. UU.
            string direccion = "701 W 2nd St, Muleshoe";
            direccion = direccion.Replace(" ","+");

            var client = new HttpClient();
            // Usa los valores para construir una URL de Google Maps que muestra en un mapa.
            string url = $"https://www.google.com/maps/place/{direccion}";
            // Crea una solicitud HTTP GET a la URL de Google Maps y envía la solicitud utilizando un objeto HttpClient.
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            // Si la respuesta es satisfactoria, extrae el contenido HTML de la respuesta y lo carga en un objeto HtmlDocument
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string htmlMaps = await response.Content.ReadAsStringAsync();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlMaps);

            // Busca en el HTML una etiqueta meta con el atributo itemprop igual a description.
            HtmlNode metaNode = document.DocumentNode.SelectSingleNode("//meta[@itemprop='description']");

            // Si la etiqueta meta es encontrada, extrae el valor del atributo content y lo muestra en la consola.
            if (metaNode != null) {
                string content = metaNode.Attributes["content"].Value;
                Console.WriteLine(content); // Dirección completa
            }

        }
    }

    /*
        By: RETBOT
    */
}
