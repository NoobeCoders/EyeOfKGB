using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crawler.DAL;
using Crawler.DAL.EntityConfigurations;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;

namespace Crawler.WebApi.Admin.Controllers
{
    public class PersonsController : ApiController
    {
        
        // GET: api/Persons
        public IEnumerable<Person> Get()
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                return dataManager.Persons.GetAll().ToList();
            }
            
        }

        // GET: api/Persons/5
        public Person Get(int id)
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                return dataManager.Persons.GetById(id);
            }
        }

        // POST: api/Persons
        public void Post(Person person)
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                dataManager.Persons.Add(person);
            }            
        }

        // PUT: api/Persons/5
        public void Put(Person person)
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                dataManager.Persons.Update(person);
            }            
        }

        // DELETE: api/Persons/5
        public void Delete(Person person)
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                dataManager.Persons.Delete(person);
            }                   
        }
    }
}
