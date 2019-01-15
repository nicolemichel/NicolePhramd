using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NicolePhramd
{
    public class Fetch
    {
        public string cs = "Server=(localdb)\\mssqllocaldb;Database=PhramdDB;Trusted_Connection=true";
        public HttpClient client = new HttpClient();
        public string Data { get; set; }
        public string Pics { get; set; }
        public string Weather { get; set; }
        public string Calendar { get; set; }
        public string News { get; set; }
        public string ToDo { get; set; }

        public async Task GrabPics()
        { }

        public async Task GrabWeather(string selCity, string selCountry, string selUnit)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));

            string wet_api = "7188b6a77c9693ed94470114f98e8761";
            HttpResponseMessage weather = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=" + selCity + "," + selCountry + "&units=" + selUnit + "&APPID=" + wet_api);

            if(weather.IsSuccessStatusCode)
            { 
                Data = await weather.Content.ReadAsStringAsync();
            }
            
        }
        public async Task GrabCal()
        { }
        public async Task GrabNews()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));

            string news_api = "64fe9e45b01d46ddb46fb999986ff796";
            HttpResponseMessage news = await client.GetAsync("https://newsapi.org/v2/top-headlines?" + "country=ca&" + "apiKey=" + news_api);

            if (news.IsSuccessStatusCode)
            {
                Data = await news.Content.ReadAsStringAsync();
            }
        }
        public async Task GrabTodo()
        { }

        public string GetData()
        {
            return Data;
        }
    }
}