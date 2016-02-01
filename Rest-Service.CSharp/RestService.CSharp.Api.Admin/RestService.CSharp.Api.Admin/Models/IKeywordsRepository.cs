using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestService.CSharp.Api.Admin.Models
{
    public interface IKeywordsRepository
    {
        IEnumerable<Keyword> Get();
        IEnumerable<Keyword> Get(int id);

        Keyword Add(Keyword item);

        bool Update(Keyword item);

        bool Delete(int id);
    }
}
