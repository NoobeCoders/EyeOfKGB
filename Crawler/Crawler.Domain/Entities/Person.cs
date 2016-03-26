using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PersonPageRank> PersonRangeRanks { get; set; }
        public virtual ICollection<Keyword> Keywords { get; set; }

        public Person()
        {
            PersonRangeRanks = new List<PersonPageRank>();
            Keywords = new List<Keyword>();
        }
    }
}
