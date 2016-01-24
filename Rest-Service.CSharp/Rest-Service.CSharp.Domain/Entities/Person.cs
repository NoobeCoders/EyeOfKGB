using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_Service.CSharp.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PersonPageRank> PersonRangeRanks { get; set; } = new List<PersonPageRank>();
        public virtual ICollection<Keyword> Keywords { get; set; } = new List<Keyword>();

    }
}
