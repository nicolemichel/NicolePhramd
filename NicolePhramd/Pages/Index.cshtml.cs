using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Timers;
using System.Globalization;

namespace NicolePhramd.Pages
{
    public class IndexModel : PageModel
    {
        JsonNinja jNinja;
        JsonNinja listNinja;
        public string display = "grid";
        public List<string> filter = new List<string>();
        public string today;
       
        // Coming from chosen email (ie. google, icloud, dropbox etc.)
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
        public string windText;
        // length of day
        public string sunrise;
        public string riseTime;
        public string sunset;
        public string setTime;
        // clouds
        public List<string> clouds = new List<string>(); // cloudiness
        public string all;
        
        // 5 day - convert to lists (new Ninja?)

        // CALENDAR
        public string calendar = "";
        //events
        //people/colours

        // NEWS
        public string selCoun;
        public string numOfArticles;
        public string headline;
        public List<string> headlineList;
        public string channel;
        public List<string> channelList;
        public string published;
        public List<string> publishedList;
        public DateTime publishedDate;
        // stop from skipping over
        public List<string> headlines = new List<string>();
        public string headlineOne;
        public string headlineTwo;
        public string headlineThree;
        public string headlineFour;
        public string headlineFive;
        public List<string> channels = new List<string>();
        public string channelOne;
        public string channelTwo;
        public string channelThree;
        public string channelFour;
        public string channelFive;
        public List<string> publishDates = new List<string>();
        public string publishedOne;
        public string publishedTwo;
        public string publishedThree;
        public string publishedFour;
        public string publishedFive;
        // headlines (timer?)
        public void Timer(int Time)
        {
            Timer tick = new Timer();
            tick.Elapsed += new ElapsedEventHandler(articleSwitch); // switches article
            tick.Interval = Time; // time interval for article switch
            // defualt 15s - set up if for 10 and 20 seconds (dropdown list)
            tick.Enabled = true; // timer is on
            tick.AutoReset = true;
        }
        private void articleSwitch(object sender, ElapsedEventArgs e)
        {
            int articleCount = Convert.ToInt32(numOfArticles);
            if (articleCount <= 5) // 5 articles
            {
                /* example
                   show article 1 00-10
                   show article 2 10-20
                   show article 3 20-30
                   show article 4 30-40
                   show article 5 50-60
                */

                for (int i = 0; i < articleCount; i++)
                {
                    headline = headlines[i];
                    channel = channels[i];
                    published = publishDates[i];
                }
            }
            else if (articleCount <= 10) // 10 articles
            {
                /* example
                   show article 6 60-70
                   show article 7 70-80
                   show article 8 80-90
                   show article 9 90-100
                   show article 10 100-110
                */

                for (int i = 0; i < articleCount; i++)
                {
                    headline = headlineList[i];
                    channel = channelList[i];
                    published = publishedList[i];
                }
            }
            else // 15 articles
            {
                /* example
                  show article 11 110-120
                  show article 12 120-130
                  show article 13 130-140
                  show article 14 140-150
                  show article 15 150-160
               */
                for (int i = 0; i < articleCount; i++)
                {
                    headline = headlineList[i];
                    channel = channelList[i];
                    published = publishedList[i];
                }
            }

        } // article switch

        //todo list
        

        public void OnPostLogin(string email, string password)
        {
            Program.User.checkUser(email, password);
            Response.Redirect("./Index");
        }

        public async Task OnPostWeather(string City, string Country, string Unit)
        {
            display = "grid";
            // HAVE TO MAKE A DEFAULT - London, ON & Metric.
                              
            // SETTINGS
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

            // Retrieve information from Weather Class
            selCity = Program.Weather.selCity;
            selCountry = Program.Weather.selCountry;
            selUnit = Program.Weather.selUnit;
            
            selCity = selCity.Replace("\"", "");
            selCountry = selCountry.Replace("\"", "");

            today = DateTime.Now.ToString("dddd, MMMM dd yyyy HH:mm tt");

            // Retrieve information from WeatherData Class
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
            windText = Program.WeatherData.windText;
            // length of day
            sunrise = Program.WeatherData.sunrise;
            riseTime = Program.WeatherData.riseTime;
            sunset = Program.WeatherData.sunset;
            setTime = Program.WeatherData.setTime;
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
            temp = wetMain[1].Replace("\"temp\":", ""); // fix! if rain and mist etc temp doesn't show properly
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

            // clouds
            clouds = listNinja.GetDetails("\"clouds\"");
            all =clouds[0].Replace("\"all\":" , "");

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

            // When Imperial is selected the input string is not the correct type & only sometimes ??
            // Break down into only N/NE/E/SE/S/SW/W/NW ?? 
            // Take out decimal values?
            double windTemp = Convert.ToDouble(windDir);
            switch (windTemp)
            {
                case double windDir when (windDir >= 348.75 && windDir <= 11.25):
                    // 348.75 - 11.25 = N
                    windText = windDir + " °N";
                    break;
                case double windDir when (windDir >=11.26 && windDir <=33.75):
                    // 11.26 - 33.75 = NNE
                    windText = windDir + " °NNE";
                    break;
                case double windDir when (windDir >= 33.76 && windDir <= 56.25):
                    // 33.76 - 56.25 = NE
                    windText = windDir + " °NE";
                    break;
                case double windDir when (windDir >= 56.26 && windDir <= 78.75):
                    // 56.26 - 78.75 = ENE
                    windText = windDir + " °ENE";
                    break;
                case double windDir when (windDir >= 78.76 && windDir <= 101.25):
                    // 78.76 - 101.25 = E
                    windText = windDir + " °E";
                    break;
                case double windDir when (windDir >= 101.26 && windDir <= 123.75):
                    // 101.26 - 123.75 = ESE
                    windText = windDir + " °ESE";
                    break;
                case double windDir when (windDir >= 123.76 && windDir <= 146.25):
                    // 123.76 - 146.25 = SE
                    windText = windDir + " °SE";
                    break;
                case double windDir when (windDir >= 146.26 && windDir <= 168.75):
                    // 146.26 - 168.75 = SSE
                    windText = windDir + " °SSE";
                    break;
                case double windDir when (windDir >= 168.76 && windDir <= 191.25):
                    // 168.76 - 191.25 = S
                    windText = windDir + " °S";
                    break;
                case double windDir when (windDir >= 191.26 && windDir <= 213.75):
                    // 191.26 - 213.75 = SSW
                    windText = windDir + " °SSW";
                    break;
                case double windDir when (windDir >= 213.76 && windDir <= 236.25):
                    // 213.76 - 236.25 = SW
                    windText = windDir + " °SW";
                    break;
                case double windDir when (windDir >= 236.26 && windDir <= 258.75):
                    // 236.26 - 258.75 = WSW
                    windText = windDir + " °WSW";
                    break;
                case double windDir when (windDir >= 258.76 && windDir <= 281.25):
                    // 258.76 - 281.25 = W
                    windText = windDir + " °W";
                    break;
                case double windDir when (windDir >= 281.26 && windDir <= 303.75):
                    // 281.26 - 303.75 = WNW
                    windText = windDir + " °WNW";
                    break;
                case double windDir when (windDir >= 303.76 && windDir <= 326.25):
                    // 303.76 - 326.25 = NW
                    windText = windDir + " °NW";
                    break;
                case double windDir when (windDir >= 326.26 && windDir <= 348.74):
                    // 326.26 - 348.75 = NNW
                    windText = windDir + " °NNW";
                    break;
                default:
                    break;
            } //windTemp() - Wind Direction

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
                    // INSERT DEFAULT VALUES OF LONDON, CANADA AND METRIC
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
        } //OnPostWeather()

        public async Task OnPostNews(string Coun, string Articles, int Time)
        {
            display = "grid";

            // SETTINGS
            Program.News.selCoun = Coun;
            Program.News.numOfArticles = Articles;

            // Pulling in information from the API
            await Program.Fetch.GrabNews(Coun, Articles);

            jNinja = new JsonNinja(Program.Fetch.Data);
            List<string> newsNames = jNinja.GetNames();
            List<string> newsVals = jNinja.GetVals();

            // Retrieve information from News Class
            selCoun = Program.News.selCoun;
            numOfArticles = Program.News.numOfArticles;

            // Grab information from NewsDataClass
            headline = Program.NewsData.headline;
            headlineList = Program.NewsData.headlineList;
            channel = Program.NewsData.channel;
            channelList = Program.NewsData.channelList;
            published = Program.NewsData.published;
            publishedList = Program.NewsData.publishedList;
            publishedDate = Program.NewsData.publishedDate;
            

            headlineList = jNinja.GetDetails("\"title\"");
            headlineOne = headlineList[0];
            headlineOne = headlineOne.Replace("\"", "");
            channelList = jNinja.GetDetails("\"name\"");
            channelOne = channelList[0];
            channelOne = channelOne.Replace("\"", "");
            publishedList = jNinja.GetDetails("\"publishedAt\"");
            publishedOne = publishedList[0];
            publishedOne = publishedOne.Replace("\"", "");
            publishedOne = publishedOne.Replace("T", " ");
            publishedOne = publishedOne.Replace("Z", "");
            publishedDate = DateTime.ParseExact(publishedOne, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // stops the next line from taking the default time value
            // ^ date format has to be in the exact format as how it is taken in - then you can change it after the fact
            publishedOne = publishedDate.ToString("dddd, MMMM dd yyyy HH:mm tt");
            headlines.Add(headlineOne);
            channels.Add(channelOne);
            publishDates.Add(publishedOne);

            headlineList = jNinja.GetDetails("\"title\"");
            headlineTwo = headlineList[1];
            headlineTwo = headlineTwo.Replace("\"", "");
            channelList = jNinja.GetDetails("\"name\"");
            channelTwo = channelList[1];
            channelTwo = channelTwo.Replace("\"", "");
            publishedList = jNinja.GetDetails("\"publishedAt\"");
            publishedTwo = publishedList[1];
            publishedTwo = publishedTwo.Replace("\"", "");
            publishedTwo = publishedTwo.Replace("T", " ");
            publishedTwo = publishedTwo.Replace("Z", "");
            publishedDate = DateTime.ParseExact(publishedTwo, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // stops the next line from taking the default time value
            // ^ date format has to be in the exact format as how it is taken in - then you can change it after the fact
            publishedTwo = publishedDate.ToString("dddd, MMMM dd yyyy HH:mm tt");
            headlines.Add(headlineTwo);
            channels.Add(channelTwo);
            publishDates.Add(publishedTwo);

            headlineList = jNinja.GetDetails("\"title\"");
            headlineThree = headlineList[2];
            headlineThree = headlineThree.Replace("\"", "");
            channelList = jNinja.GetDetails("\"name\"");
            channelThree = channelList[2];
            channelThree = channelThree.Replace("\"", "");
            publishedList = jNinja.GetDetails("\"publishedAt\"");
            publishedThree = publishedList[2];
            publishedThree = publishedThree.Replace("\"", "");
            publishedThree = publishedThree.Replace("T", " ");
            publishedThree = publishedThree.Replace("Z", "");
            publishedDate = DateTime.ParseExact(publishedThree, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // stops the next line from taking the default time value
            // ^ date format has to be in the exact format as how it is taken in - then you can change it after the fact
            publishedThree = publishedDate.ToString("dddd, MMMM dd yyyy HH:mm tt");
            headlines.Add(headlineThree);
            channels.Add(channelThree);
            publishDates.Add(publishedThree);

            headlineList = jNinja.GetDetails("\"title\"");
            headlineFour = headlineList[3];
            headlineFour = headlineFour.Replace("\"", "");
            channelList = jNinja.GetDetails("\"name\"");
            channelFour = channelList[3];
            channelFour = channelFour.Replace("\"", "");
            publishedList = jNinja.GetDetails("\"publishedAt\"");
            publishedFour = publishedList[3];
            publishedFour = publishedFour.Replace("\"", "");
            publishedFour = publishedFour.Replace("T", " ");
            publishedFour = publishedFour.Replace("Z", "");
            publishedDate = DateTime.ParseExact(publishedFour, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // stops the next line from taking the default time value
            // ^ date format has to be in the exact format as how it is taken in - then you can change it after the fact
            publishedFour = publishedDate.ToString("dddd, MMMM dd yyyy HH:mm tt");
            headlines.Add(headlineFour);
            channels.Add(channelFour);
            publishDates.Add(publishedFour);

            headlineList = jNinja.GetDetails("\"title\"");
            headlineFive = headlineList[4];
            headlineFive = headlineFive.Replace("\"", "");
            channelList = jNinja.GetDetails("\"name\"");
            channelFive = channelList[4];
            channelFive = channelFive.Replace("\"", "");
            publishedList = jNinja.GetDetails("\"publishedAt\"");
            publishedFive = publishedList[4];
            publishedFive = publishedFive.Replace("\"", "");
            publishedFive = publishedFive.Replace("T", " ");
            publishedFive = publishedFive.Replace("Z", "");
            publishedDate = DateTime.ParseExact(publishedFive, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // stops the next line from taking the default time value
            // ^ date format has to be in the exact format as how it is taken in - then you can change it after the fact
            publishedFive = publishedDate.ToString("dddd, MMMM dd yyyy HH:mm tt");
            headlines.Add(headlineFive);
            channels.Add(channelFive);
            publishDates.Add(publishedFive);















            Timer(Time);

            // CREATE THE DATABASE FOR THE NEWS SETTINGS
            // country
            // number of articles to swap through (timer based)
            // INSERT DEFAULT OF CANADA AND 20
        } //OnPostNews()
        
        public void OnGet()
        { }

    } // class
} // namespace
