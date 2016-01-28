using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.DBInitializers
{
    class InitSites : InitializationDB
    {
        override public void Initialization(ApplicationDbContext context)
        {
            Site metanit = new Site()
            {
                Name = "metanit.com"
            };

            Site professorWeb = new Site()
            {
                Name = "professorweb.ru"
            };

            Site geekBrains = new Site()
            {
                Name = "geekbrains.ru"
            };

            Site oper = new Site()
            {
                Name = "oper.ru"
            };

            //Site localhost = new Site()
            //{
            //    Name = "localhost",
            //    Pages = new List<Page>()
            //};

            context.Sites.Add(oper);
            //context.Sites.Add(metanit);
            //context.Sites.Add(professorWeb);
            //context.Sites.Add(geekBrains);

            //context.Sites.Add(localhost);

            context.SaveChanges();
        }
    }
}
