using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person GetPersonByName(string name);
    }
}
