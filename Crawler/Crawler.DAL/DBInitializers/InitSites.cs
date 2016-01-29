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
            Site geekbrains = new Site()
            {
                Name = "geekbrains.ru"
            };

            Site professorweb = new Site()
            {
                Name = "professorweb.ru"
            };

            Site lentaRu = new Site()
            {
                Name = "lenta.ru"
            };

            Site metanit = new Site()
            {
                Name = "metanit.com"
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

            context.Sites.Add(lentaRu);
            //context.Sites.Add(geekbrains);
            //context.Sites.Add(metanit);
            //context.Sites.Add(professorWeb);
            //context.Sites.Add(geekBrains);

            //context.Sites.Add(localhost);

            context.SaveChanges();
        }
    }
}
