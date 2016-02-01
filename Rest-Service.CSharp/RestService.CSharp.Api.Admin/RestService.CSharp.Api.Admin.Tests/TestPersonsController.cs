using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestService.CSharp.Api.Admin.Controllers;
using RestService.CSharp.Api.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace RestService.CSharp.Api.Admin.Tests
{
    [TestClass]
    public class TestPersonsController
    {
        [TestMethod]
        public void Get_All_Persons_From_Fake_Repository()
        {

            var persons = GetPersons();
            PersonsController controller = new PersonsController();
            
            
            var jsonPersons = Json.Encode(persons);
            var result = Json.Encode(controller.Get());
            

            Assert.AreEqual(jsonPersons, result);

        }

        [TestMethod]
        public void Get_Person_By_ID_From_Fake_Repository()
        {
            Person person = new Person(3, "Навальный");
            PersonsController controller = new PersonsController();

            var jsonPerson = Json.Encode(person);
            var result = Json.Encode(controller.Get(3));

            Assert.AreEqual(jsonPerson, result);
        }

        [TestMethod]
        public void Post_Person_To_Fake_Repository()
        {
            Person person = new Person();
            person.Name = "Жириновский";

            PersonsController controller = new PersonsController();

            var result = controller.Post(person);

            Assert.AreEqual(4,result.PersonID);
            Assert.AreEqual("Жириновский", result.Name);
        }


        [TestMethod]
        public void Put_Person_To_Fake_Repository()
        {
            Person person = new Person();
            person.PersonID = 2;
            person.Name = "Жириновский";
            
            PersonsController controller = new PersonsController();
            controller.Put(person);

            var result = controller.Get(2);

            string personJson = Json.Encode(person);
            string resultJson = Json.Encode(result);

            Assert.AreEqual(personJson, resultJson);

        }


        [TestMethod]
        public void Delete_Person_From_Fake_Repository()
        {
            var persons = GetPersons();

            PersonsController controller = new PersonsController();
            controller.Delete(persons.First(p => p.PersonID==2).PersonID);

            var result = controller.Get();

            Assert.AreEqual(2, result.Count());
            Assert.AreNotEqual(persons.Count(),result.Count());
            Assert.AreNotEqual(Json.Encode(persons), Json.Encode(result));
        }

        public IEnumerable<Person> GetPersons()
        {
            return  new List<Person>()
                                        {
                                            new Person(1, "Путин"),
                                            new Person(2, "Медведев"),
                                            new Person(3, "Навальный")
                                        };
        }
    }
}
