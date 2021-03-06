﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NicolePhramd.Utility;

namespace NicolePhramd
{
    public class Program
    {
        public static Fetch Fetch = new Fetch();
        public static User User = new User();
        public static Weather Weather = new Weather();
        public static WeatherData WeatherData = new WeatherData();
        public static News News = new News();
        public static NewsData NewsData= new NewsData();

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
