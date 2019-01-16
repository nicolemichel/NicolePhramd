using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NicolePhramd
{
    public class NewsData
    {
        public List<string> headline { get; set; }
        public List<string> channel { get; set; }
        public List<string> published { get; set; }
        public DateTime day { get; set; }
    }
}