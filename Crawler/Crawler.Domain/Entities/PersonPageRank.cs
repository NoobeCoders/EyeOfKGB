using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class PersonPageRank
    {
        public int PersonID { get; set; }
        public int PageID { get; set; }
        public int Rank { get; set; }
    }
}
