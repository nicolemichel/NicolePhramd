using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NicolePhramd.Utility
{
    public class WeatherData
    {        
        // WEATHER


        // MAIN
        public string main { get; set; }
        public string temp { get; set; } 
        public string tempHigh { get; set; }
        public string tempLow { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string visibility { get; set; }
        public string desc { get; set; }

    }
}