using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppMAUI.Models;
using DotNetEnv;

namespace WeatherAppMAUI.Service
{
    internal class DataService
    {
        public static async Task<Tempo?> GetPrevisaoDoTempo(string cidade)
        {
            //var apiKey = Environment.GetEnvironmentVariable("Weather_API_Key"); // Pega a API key do .env
            var apiKey = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"http://api.openweathermap.org/data/2.5/" + $"weather?q={cidade}&units=metric&appid={apiKey}";

            Tempo tempo = null;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    var rascunho = JObject.Parse(json);

                    DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();
                    DateTime dataPesquisa = DateTime.Now.Date;


                    tempo = new()
                    {
                        Humidity = (string)rascunho["main"]["humidity"],
                        Temperature = (string)rascunho["main"]["temp"],
                        Title = (string)rascunho["name"],
                        Visibility = (string)rascunho["visibility"],
                        Wind = (string)rascunho["wind"]["speed"],
                        Sunrise = sunrise.ToString(),
                        Sunset = sunset.ToString(),
                        Weather = (string)rascunho["weather"][0]["main"],
                        code = (string)rascunho["weather"][0]["id"],
                        WeatherDescription = (string)rascunho["weather"][0]["description"],
                        DataPesquisa = dataPesquisa.ToString()
                    };
                } // Fecha if
            } // Fecha laço using

            return tempo;
        }
    }
}
