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
        

    } // class
} // namespace