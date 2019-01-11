﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace NicolePhramd.Utility
{
    public class WeatherData
    {
        // WEATHER
        public List<string> weather = new List<string>();
        public string wetId { get; set; } // weather condidtion id
        public List<string> wetMain { get; set; } // List name - pulls snow and temp.
        public string desc { get; set; } // weather desc ie. Light snow
        public List<string> dayIcon { get; set; } // sun/moon/clouds etc.
        public string icon;
        public string iconShow;
        // MAIN
        public string temp { get; set; }
        public string tempHigh { get; set; }
        public string tempLow { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string visibility { get; set; }
        // WIND
        public List<string> wind = new List<string>();
        public string windSpeed { get; set; }
        public string windDir { get; set; } // in degrees - set up if statement to get N/E/S/W
        // LENGTH OF DAY
        public string sunrise { get; set; }
        public string riseTime { get; set; }
        public string sunset { get; set; }
        public string setTime { get; set; }
        // RAIN
        public List<string> rain = new List<string>(); // "list"
        public string rain3h { get; set; } // volume last 3 hours
        // SNOW
        public List<string> snow = new List<string>(); // "list"
        public string snow3h { get; set; }
        // clouds
        public List<string> clouds = new List<string>(); // cloudiness
        public string all { get; set; }

    }
}