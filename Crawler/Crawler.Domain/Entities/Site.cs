using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Page> Pages { get; set; }

        public Site()
        {
            Pages = new List<Page>();
        }
    }
}
