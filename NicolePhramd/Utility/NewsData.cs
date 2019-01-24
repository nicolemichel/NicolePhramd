using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;


namespace NicolePhramd
{
    public class NewsData
    {
        public string headline { get; set; }
        public List<string> headlineList { get; set; }
        public string channel { get; set; }
        public List<string> channelList { get; set; }
        public string published { get; set; }
        public List<string> publishedList { get; set; }
        public DateTime publishedDate { get; set; }

        // stop from skipping over
        public List<string> headlines { get; set; }
        public string headlineOne { get; set; }
        public string headlineTwo { get; set; }
        public string headlineThree { get; set; }
        public string headlineFour { get; set; }
        public string headlineFive { get; set; }
        public List<string> channels { get; set; }
        public string channelOne { get; set; }
        public string channelTwo { get; set; }
        public string channelThree { get; set; }
        public string channelFour { get; set; }
        public string channelFive { get; set; }
        public List<string> publishDates { get; set; }
        public string publishedOne { get; set; }
        public string publishedTwo { get; set; }
        public string publishedThree { get; set; }
        public string publishedFour { get; set; }
        public string publishedFive { get; set; }

    } // class
} // namespace