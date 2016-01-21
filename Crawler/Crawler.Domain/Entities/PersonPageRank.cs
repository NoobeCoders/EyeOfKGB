using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class PersonPageRank
    {
        public int Rank { get; set; }

        public virtual int PageId { get; set; }
        public virtual Page Pages { get; set; }

        public virtual int PersonId { get; set; }
        public virtual Person Persons { get; set; }
    }
}
