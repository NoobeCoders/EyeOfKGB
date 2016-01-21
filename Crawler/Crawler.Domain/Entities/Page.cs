using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class Page
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public int SiteID { get; set; }
        public DateTime FoundDateTime { get; set; }
        public DateTime LastSacnDate { get; set; }
    }
}
