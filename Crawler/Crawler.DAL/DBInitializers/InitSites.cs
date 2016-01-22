﻿using Crawler.Domain.Entities;
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
            Site lentaRU = new Site()
            {
                Name = "Lenta.ru",
                Pages = new List<Page>()
                {
                    new Page() { URL = "lenta.ru", FoundDateTime = DateTime.Now }
                }
            };

            Site vestiRU = new Site()
            {
                Name = "Lenta.ru",
                Pages = new List<Page>()
                {
                    new Page() { URL = "vesti.ru", FoundDateTime = DateTime.Now }
                }
            };

            context.Sites.Add(lentaRU);
            context.Sites.Add(vestiRU);

            context.SaveChanges();
        }
    }
}