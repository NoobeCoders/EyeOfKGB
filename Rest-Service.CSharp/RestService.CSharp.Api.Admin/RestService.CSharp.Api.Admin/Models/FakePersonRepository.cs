using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.CSharp.Api.Admin.Models
{
    public class FakePersonRepository : IPersonsRepository
    {
        private List<Person> persons = new List<Person>()
                                                            {
                                                                new Person(1, "Путин"),
                                                                new Person(2, "Медведев"),
                                                                new Person(3, "Навальный")
                                                            };
        public IEnumerable<Person> Get()
        {
            return persons;
        }

        public Person Get(int id)
        {
            return persons.First(p=>p.PersonID==id);
        }

        public Person Add(Person item)
        {
            lock (persons)
            {
                item.PersonID = persons.Count + 1;
                persons.Add(item);
            }
            return item;
        }

        public bool Update(Person item)
        {
            lock (persons)
            {
                Person storedPerson = Get(item.PersonID);
                if (storedPerson != null)
                {
                    storedPerson.Name = item.Name;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Delete(int id)
        {
            lock (persons)
            {
                Person storedPerson = Get(id);
                if (storedPerson != null)
                {
                    persons.Remove(storedPerson);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}