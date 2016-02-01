using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestService.CSharp.Api.Admin.Models
{
    public interface IPersonsRepository
    {
        IEnumerable<Person> Get();
        Person Get(int id);

        Person Add(Person item);

        bool Update(Person item);
        
        bool Delete(int id);
                
    }
}
