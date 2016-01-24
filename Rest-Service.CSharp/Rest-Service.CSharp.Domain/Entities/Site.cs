using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest_Service.CSharp.Domain.Entities
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    }
}
