# De-Coordenadas-a-Direccion
El código obtiene la dirección de una ubicación en Google Maps a partir de sus coordenadas de latitud y longitud y la muestra en la consola.

```csharp
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
// By: RETBOT
namespace Coordenadas
{
    class Program
    {
        /*
         * El código obtiene la dirección de una ubicación en Google Maps a partir de sus coordenadas de latitud y longitud 
         * y la muestra en la consola.
         */
        static async Task Main(string[] args)
        {
            //  Se definen los valores de latitud y longitud y los almacena en las variables
            double lat = 19.4328445;
            double lon = -99.1357453;

            // Llama a la función FormatCoord y le pasa los valores de latitud y longitud como argumentos para formatearlos como coordenadas.
            string formatCoord = FormatCoord(lat, lon);
            var client = new HttpClient();
            // Usa los valores formateados de latitud y longitud para construir una URL de Google Maps que muestra las coordenadas en un mapa.
            string url = $"https://www.google.com/maps/place/{formatCoord}/@{lat},{lon}";
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
                Console.WriteLine(content); // Dirección 
            }

        }


        /*
         * FormatCoord toma dos valores de latitud y longitud como entrada y los formatea en grados, minutos y segundos con dirección cardinal. 
         */
        public static string FormatCoord(double latitude, double longitude)
        {
            // Latitude
            int degreeLat = (int)latitude; // Obtiene la parte entera de la latitud
            double tempLat = (latitude - degreeLat) * 60; // Obtiene los minutos de la latitud
            int minutesLat = (int)tempLat; // Convierte los minutos de la latitud a entero
            double secondsLat = (tempLat - minutesLat) * 60; // Obtiene los segundos de la latitud

            string lat = Math.Abs(degreeLat) + "°" + Math.Abs(minutesLat).ToString("00") + "'" + Math.Abs(secondsLat).ToString("00.0") + "\""; // Construye la cadena de texto que representa la latitud formateada
            if (degreeLat < 0)
                lat += "S"; // Agrega la dirección cardinal correspondiente a la latitud
            else
                lat += "N";

            // Longitude 
            int degreeLon = (int)longitude; // Obtiene la parte entera de la longitud
            double tempLon = (longitude - degreeLon) * 60; // Obtiene los minutos de la longitud
            double minutesLon = (int)tempLon; // Convierte los minutos de la longitud a entero
            double secondsLon = (tempLon - minutesLon) * 60; // Obtiene los segundos de la longitud

            string lon = Math.Abs(degreeLon) + "°" + Math.Abs(minutesLon).ToString("00") + "'" + Math.Abs(secondsLon).ToString("00.0") + "\""; // Construye la cadena de texto que representa la longitud formateada
            if (degreeLon < 0)
                lon += "W"; // Agrega la dirección cardinal correspondiente a la longitud
            else
                lon += "E";

            return $"{lat}+{lon}"; // Devuelve la cadena de texto completa que representa las coordenadas en formato de grados, minutos y segundos con dirección cardinal.
        }
    }

    /*
        By: RETBOT
    */
}


Nota: Fue creado como una alternativa a las APIs de pago y, por lo tanto, se puede utilizar de manera gratuita. Sin embargo, es importante tener en cuenta que su uso excesivo puede saturar el servidor de Google Maps y afectar el rendimiento de otras aplicaciones. Se recomienda usar este código con precaución y respetando los límites de uso establecidos por Google.
