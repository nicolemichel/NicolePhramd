using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace NicolePhramd.Models
{
    public class WeatherDB
    {
        public int id { get; set; }

        [Required]
        public string country { get; set; }
        [Required]
        public string city {get;set;}
        [Required]
        public string unit { get; set; }
        [Required]
        public string status { get; set; }

        public int userId { get; set; }
        public User user { get; set; }

    }
}
