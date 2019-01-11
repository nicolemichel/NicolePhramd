using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace NicolePhramd.Pages
{
    public class IndexModel : PageModel
    {
        JsonNinja jNinja;
        JsonNinja listNinja;
        public string display = "grid";
        public List<string> filter = new List<string>();
        public string today;

        // Coming from google photos
        public List<string> pictures = new List<string>(); // background photos

        // WEATHER \\
        // location
        // Coming from Weather Class
        public string selCity;
        public string selCountry;
        public string selUnit;
        public string metric;
        public string imperial;
        public string kelvin;

        // Coming from WeatherData Class
        // weather
        public List<string> weather = new List<string>();
        public string wetId; // weather condition id
        // pull icons based off these ???? switch statement (probably better to just use the icon # that gets pulled in)
        public List<string> wetMain; // weather parameter ie. rain
        public string desc; // condition in group (light/hevy/thunderstorm)
        public List<string> dayIcon; // weather icon of day
        public string icon;
        public string iconShow;
        // main
        public string temp;
        public string tempHigh;
        public string tempLow; 
        public string humidity;
        public string pressure;
        public string visibility;
        // wind
        public List<string> wind = new List<string>();
        public string windSpeed;
        public string windDir; // in degrees - set up if statement to get N/E/S/W
        public string windGust;
        // length of day
        public string sunrise;
        public string riseTime;
        public string sunset;
        public string setTime;
        // rain
        public List<string> rain = new List<string>(); // "list"
        public string rain3h; // volume last 3 hours
        // snow
        public List<string> snow = new List<string>(); // "list"
        public string snow3h;
        // clouds
        public List<string> clouds = new List<string>(); // cloudiness
        public string all;
        
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

            //Retrieve information from Weather Class
            selCity = Program.Weather.selCity;
            selCountry = Program.Weather.selCountry;
            selUnit = Program.Weather.selUnit;
            
            selCity = selCity.Replace("\"", "");
            selCountry = selCountry.Replace("\"", "");

            today = DateTime.Now.ToString("dddd, MMMM dd yyyy HH:mm tt");

            //Retrieve information from WeatherData Class
            // weather
            weather = Program.WeatherData.weather;
            wetId = Program.WeatherData.wetId;
            wetMain = Program.WeatherData.wetMain;
            desc = Program.WeatherData.desc;
            dayIcon = Program.WeatherData.dayIcon;
            icon = Program.WeatherData.icon;
            iconShow = Program.WeatherData.iconShow;
            // main
            temp = Program.WeatherData.temp;
            tempHigh = Program.WeatherData.tempHigh;
            tempLow = Program.WeatherData.tempLow;
            humidity = Program.WeatherData.humidity;
            pressure = Program.WeatherData.pressure;
            visibility = Program.WeatherData.visibility;
            // wind
            wind = Program.WeatherData.wind;
            windSpeed = Program.WeatherData.windSpeed;
            windDir = Program.WeatherData.windDir;
            // length of day
            sunrise = Program.WeatherData.sunrise;
            riseTime = Program.WeatherData.riseTime;
            sunset = Program.WeatherData.sunset;
            setTime = Program.WeatherData.setTime;
            // rain
            rain = Program.WeatherData.rain;
            rain3h = Program.WeatherData.rain3h;
            // snow
            snow = Program.WeatherData.snow;
            snow3h = Program.WeatherData.snow3h;
            // clouds
            clouds = Program.WeatherData.clouds;
            all = Program.WeatherData.all;


            // weather
            listNinja = new JsonNinja(Program.Fetch.Data);
            weather = listNinja.GetDetails("\"weather\"");
            wetId = jNinja.GetInfo("\"id\""); // might not need
            wetMain = jNinja.GetDetails("\"main\""); // ie. rain
            desc = jNinja.GetInfo("\"description\""); // ie. light rain
            desc = desc.Replace("\"", "");
            dayIcon = listNinja.GetDetails("\"icon\"");
            icon = dayIcon[0].Replace("\"]", "");
            icon = icon.Replace("\"", "");
            iconShow = "http://openweathermap.org/img/w/" + icon + ".png";


            // main
            temp = wetMain[1].Replace("\"temp\":", "");
            tempHigh = jNinja.GetInfo("\"temp_max\"");
            tempLow = jNinja.GetInfo("\"temp_min\"");
            humidity = jNinja.GetInfo("\"humidity\"");
            pressure = jNinja.GetInfo("\"pressure\"");
            visibility = jNinja.GetInfo("\"visibility\"");

            // wind
            wind = listNinja.GetDetails("\"wind\"");
            windSpeed = wind[0].Replace("\"speed\":", "");
            windDir = jNinja.GetInfo("\"deg\"");

            // length of day
            sunrise = jNinja.GetInfo("\"sunrise\"");
            riseTime = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(sunrise) * 1000).ToLocalTime().ToLongTimeString();
            sunset = jNinja.GetInfo("\"sunset\"");
            setTime = (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(sunset) * 1000).ToLocalTime().ToLongTimeString();

            // DO NOT COME IN UNLESS IT HAS BEEN RAINING OR SNOWING
            // If/Else statement for display
            // rain
            // rain = listNinja.GetDetails("\"rain\"");
            // rain3h = rain[0].Replace("\"3h\":", "");

            // snow
            // snow = listNinja.GetDetails("\"snow\"");
            // snow3h = snow[0].Replace("\"3h\":", "");

            // clouds
            clouds = listNinja.GetDetails("\"clouds\"");
            all =clouds[0].Replace("\"all\":" , "");



            // UNIT IF/ELSE TO SHOW PROPER DATA (Metric/Imperial)
            // taking out the Kelvin option (breaks down the information the same way Metric does)
            metric = Program.Weather.metric;
            imperial = Program.Weather.imperial;
            kelvin = Program.Weather.kelvin;

            if (Unit == metric) // Metric
            {
                temp = temp + "°C";
                tempHigh = tempHigh + "°C";
                tempLow = tempLow + "°C";
                visibility = visibility + " meters";
                windSpeed = windSpeed + " meters/second";
                
                           
            }
            else if (Unit == imperial)
            {
                temp = temp + "°F";
                tempHigh = tempHigh + "°F";
                tempLow = tempLow + "°F";
                visibility = visibility + " feet";
                windSpeed = windSpeed + " miles/hour";                
            }
            else // Kelvin
            {
                temp = temp + "°K";
                tempHigh = tempHigh + "°K";
                tempLow = tempLow + "°K";
                visibility = visibility + " meters";
                windSpeed = windSpeed + " meters/second";
            }

            // Convert to Double then assign a String.
            double wTemp = Convert.ToDouble(windDir);
            switch (windDir)
            {
                case "N":
                    // 348.75 - 11.25
                    windDir = "N"; 
                    break;
                case "NNE":
                    // 11.26 - 33.75
                    break;
                case "NE":
                    // 33.76 - 56.25
                    break;
                case "ENE":
                    // 56.26 - 78.75
                    break;
                case "E":
                    // 78.76 - 101.25
                    break;
                case "ESE":
                    // 101.26 - 123.75
                    break;
                case "SE":
                    // 123.76 - 146.25
                    break;
                case "SSE":
                    // 146.26 - 168.75
                    break;
                case "S":
                    // 168.76 - 191.25
                    break;
                case "SSW":
                    // 191.26 - 213.75
                    break;
                case "SW":
                    // 213.76 - 236.25
                    break;
                case "WSW":
                    // 236.26 - 258.75
                    break;
                case "W":
                    // 258.76 - 281.25
                    break;
                case "WNW":
                    // 281.26 - 303.75
                    break;
                case "NW":
                    // 303.76 - 326.25
                    break;
                case "NNW":
                    // 326.26 - 348.75
                    break;
                default:
                    break;
            }


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
