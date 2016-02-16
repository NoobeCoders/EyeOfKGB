using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSPersonRepository : IPersonRepository
    {
        ICustomWebClient webClient;

        public WSPersonRepository(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IEnumerable<Person> GetAll()
        {
            string answer = webClient.GetRequest("/api/persons/GetPersons");
            return Json.Decode<List<Person>>(answer);
        }

        public Person GetPersonByName(string name)
        {
            IEnumerable<Person> persons = GetAll();
            return persons.FirstOrDefault(p => p.Name == name);
        }

        public Person GetById(int id)
        {
            IEnumerable<Person> persons = GetAll();
            return persons.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Person person)
        {
            webClient.PostRequest("/api/persons/PostPerson/", Json.Encode(person));
        }

        public void Update(Person person)
        {
            webClient.PutRequest("/api/persons/PutPerson/", Json.Encode(person));
        }

        public void Delete(int id)
        {
            webClient.DeleteRequest("/api/persons/DeletePerson/", Json.Encode(id));
        }

        public void Delete(Person person)
        {
            Delete(person.Id);
        }
    }
}
