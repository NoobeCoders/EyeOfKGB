using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Crawler.DAL.Implementaions
{
    public class EFPersonRepository : IPersonRepository
    {
        ApplicationDbContext dbContext;

        public EFPersonRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Person GetPersonByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            throw new NotImplementedException();
        }

        public Person GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Person item)
        {
            throw new NotImplementedException();
        }

        public void Update(Person item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person item)
        {
            throw new NotImplementedException();
        }
    }
}
