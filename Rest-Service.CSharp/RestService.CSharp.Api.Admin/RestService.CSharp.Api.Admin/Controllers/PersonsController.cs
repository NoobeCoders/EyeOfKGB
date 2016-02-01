using RestService.CSharp.Api.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.CSharp.Api.Admin.Controllers
{
    public class PersonsController : ApiController
    {
        
        IPersonsRepository repository = new FakePersonRepository();
        // GET api/persons
        public IEnumerable<Person> Get()
        {
            return repository.Get();
        }

        // GET api/persons/5
        public Person Get(int id)
        {
            return repository.Get(id);
        }

        // POST api/persons
        public Person Post(Person person)
        {
            return repository.Add(person);
        }

        // PUT api/persons/5
        public bool Put(Person person)
        {
            return repository.Update(person);
        }

        // DELETE api/persons/5
        public bool Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
