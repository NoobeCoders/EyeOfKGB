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
            Person putin = new Person()
            {
                Name = "Владимир Владимирович Путин",
                Keywords = new List<Keyword>()
                {
                    new Keyword() { Name = "Путин" },
                    new Keyword() { Name = "Путином" },
                    new Keyword() { Name = "Путина" }
                }
            };

            context.Persons.Add(putin);
            context.SaveChanges();
        }
    }
}
