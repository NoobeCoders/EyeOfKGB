using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return dbContext.Persons.FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Person> GetAll()
        {
            return dbContext.Persons;
        }

        public Person GetById(int id)
        {
            return dbContext.Persons.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Person item)
        {
            dbContext.Persons.Add(item);
        }

        public void Update(Person item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Person item)
        {
            Delete(item.Id);
        }


        public void Delete(int id)
        {
            Person person = GetById(id);
            if (person != null)
                dbContext.Persons.Remove(person);
        }
    }
}
