using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using NicolePhramd.Utility;

namespace NicolePhramd.Pages
{
    public class IndexModel : PageModel
    {
        JsonNinja jNinja;
        JsonNinja listNinja;
        public string display = "grid";
        public List<string> filter = new List<string>();
        
        // Coming from google photos
        public List<string> pictures = new List<string>(); // background photos

        // WEATHER \\
        // location
        // Coming from Weather Class
        public string selCity;
        public string selCountry;
        public string selUnit;

        // Coming from WeatherData Class
        public string selLang; // language - only applied to description (do we need????)
        
        // main
        public List<string> main; // main weather of day, ie. Fog
        public string temp;
        public string tempHigh;
        public string tempLow; 
        public string humidity;
        public string pressure;
        public string visibility;
        public string desc; // ie. cloudy
        // wind
        public List<string> wind;
        public string windSpeed;
        public string windDir; // in degrees - set up if statement to get N/E/S/W
        public string windGust;
        // weather
        public List<string> weather;
        public string wetId; // weather condition id
        // pull icons based off these ???? switch statement (probably better to just use the icon # that gets pulled in)
        public string wetMain; // weather parameter ie. rain
        public string wetDesc; // condition in group (light/hevy/thunderstorm)
        public string dayIcon; // weather icon of day
        // length of day
        public string sunrise;
        public string sunset;
        // rain
        public List<string> rain;
        public string rain3h; // volume last 3 hours
        // snow
        public List<string> snow;
        public string snow3h;
        // clouds
        public List<string> clouds; // cloudiness
        public string all;

        // WEATHER CONDITION CODES
        // clouds/cloudiness 801 - 804 (few - overcast)
        // rain 500 - 531 (light - ragged shower)
        // thunderstorm 200 - 232 (light rain to heavy drizzle)
        // drizzle 300 - 321 (light intensity - shower)
        // snow 600 - 622 (light - heavy shower)
        // atmosphere 701 - 781 (mist - tornado)
        // clear 800

        // DO WE WANT TO INCLUDE
        // atmospheric pressure @
        // sealvl
        // grndlvl

        // 5 day - convert to lists (new Ninja?)

        public string calendar = "";
        //events
        //people/colours

        public string news = "";
        //headlines (timer?)

        //todo list

        //date&time
        //timers/countdowns ???

        public void OnPostLogin(string email, string password)
        {
            Program.User.checkUser(email, password);
            Response.Redirect("./Index");
        }

        public async Task OnPostWeather(string City, string Country, string Unit)
        {
            display = "grid";
            // make a default of London, CA & metric.
                                   
            Program.Weather.selCity = City;
            Program.Weather.selCountry = Country;
            Program.Weather.selUnit = Unit;

            // Pulling in information from the API
            await Program.Fetch.GrabWeather(City, Country, Unit);
            
            jNinja = new JsonNinja(Program.Fetch.Data);
            List<string> names = jNinja.GetNames();
            List<string> vals = jNinja.GetVals();
                       
            Program.Weather.selCity = jNinja.GetInfo("\"name\"");
            Program.Weather.selCountry = jNinja.GetInfo("\"country\"");
            // not in the api, choice has to be inserted into the api call????
            //Program.Weather.selUnit = jNinja/GetInfo("\"\"");
            // Also have to make sense of the data - lots of random numbers (add degrees etc.)
            // Need a switch for the display (if C/F/Kelvin) to show the change/unit

            selCity = Program.Weather.selCity;
            selCountry = Program.Weather.selCountry;
            selUnit = Program.Weather.selUnit;
            
            selCity = selCity.Replace("\"", "");
            selCountry = selCountry.Replace("\"", "");

            // weather
            listNinja = new JsonNinja(Program.Fetch.Data);
            weather = listNinja.GetDetails("\"weather\"");
            // filter = listNinja.GetDetails(filter[2]);
            wetId = jNinja.GetInfo("\"id\"");
            wetMain = jNinja.GetInfo("\"main\""); // ie. rain
            wetDesc = jNinja.GetInfo("\"description\""); // ie. light rain
            dayIcon = jNinja.GetInfo("\"icon\"");

            // main
            main = jNinja.GetDetails("\"main\""); // "list" name
            temp = jNinja.GetInfo("\"temp\""); // try replacing : with ""
            tempHigh = jNinja.GetInfo("\"temp_max\"");
            tempLow = jNinja.GetInfo("\"temp_min\"");
            humidity = jNinja.GetInfo("\"humidity\"");
            pressure = jNinja.GetInfo("\"pressure\"");
            visibility = jNinja.GetInfo("\"visibility\"");
                                  
            // wind
            windSpeed = jNinja.GetInfo("\"speed\"");
            windDir = jNinja.GetInfo("\"deg\"");
            //windGust = jNinja.GetInfo("\"gust\""); need? same as speed

            // length of day
            sunrise = jNinja.GetInfo("\"sunrise\"");
            sunset = jNinja.GetInfo("\"sunset\"");
                        
            // rain
            //rain3h = jNinja.GetInfo("\"3h\"");

            // snow
            //snow3h = jNinja.GetInfo("\"3h\"");

            // clouds
            //clouds = "";


            // userId is needed for saving changes to that user only (will be needed for all changes)
            int userId = 1; // grab from the page ????

            if (userId == 0) // not logged in
            {
                // only display the home page - no settings AKA no settings page to see these options.
            }

            else
            {
                using (SqlConnection myConn = new SqlConnection(Program.Fetch.cs))
                {
                    SqlCommand getWeather = new SqlCommand
                    {
                        Connection = myConn
                    };
                    myConn.Open();
                    
                    // Put in same order as the SP & Table (maybe change userId to last - since it's a FK ??)
                    getWeather.Parameters.AddWithValue("@userId", userId);
                    getWeather.Parameters.AddWithValue("@country", selCountry);
                    getWeather.Parameters.AddWithValue("@city", selCity);
                    getWeather.Parameters.AddWithValue("@unit", Unit);

                    getWeather.CommandText = ("[spWeatherSettings]");
                    getWeather.CommandType = System.Data.CommandType.StoredProcedure;

                    getWeather.ExecuteNonQuery();

                    myConn.Close();
                }
            }
            // Refresh the settings page @ weather pos on page

        }

        public void OnGet()
        { }

    }
}
