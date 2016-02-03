using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class DisallowPattern
    {
        public int Id { get; set; }
        public string Pattern { get; set; }
        public string Agent { get; set; }

        public int SiteId { get; set; }
        public Site Site { get; set; }
    }
}
