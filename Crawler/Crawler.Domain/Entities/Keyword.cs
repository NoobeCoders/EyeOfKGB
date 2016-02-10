using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
