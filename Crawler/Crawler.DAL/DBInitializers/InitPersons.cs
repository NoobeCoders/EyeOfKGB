using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.DBInitializers
{
    class InitPersons : InitializationDB
    {
        override public void Initialization(ApplicationDbContext context)
        {
            Person poly = new Person()
            {
                Name = "Полиморфизм",
                Keywords = new List<Keyword>()
                {
                    new Keyword() { Name = "полиморфизм" },
                    new Keyword() { Name = "полиморфизму" },
                    new Keyword() { Name = "полиморфизма" }
                }
            };

            Person putin = new Person()
            {
                Name = "Путин Владимир Владимирович",
                Keywords = new List<Keyword>()
                {
                    new Keyword() { Name = "Путин" },
                    new Keyword() { Name = "Путину" },
                    new Keyword() { Name = "Путина" }
                }
            };

            context.Persons.Add(putin);
            context.Persons.Add(poly);
            context.SaveChanges();
        }
    }
}
