using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.CSharp.Api.Admin.Models
{
    public class Person
    {
        public Person() { }

        public Person(int personID, string name)
        {
            PersonID = personID;
            Name = name;
        }

        public int PersonID { get; set; }
        public string Name { get; set; }
    }
}