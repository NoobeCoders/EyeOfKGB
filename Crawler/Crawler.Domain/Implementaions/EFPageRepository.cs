﻿using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Crawler.Domain.Implementaions
{
    public class EFPageRepository : IPageRepository
    {
        ApplicationDbContext dbContext;

        public EFPageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Page> GetPagesByFounDateTime(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetPagesByLastScanDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetAll()
        {
            throw new NotImplementedException();
        }

        public Page GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Page item)
        {
            throw new NotImplementedException();
        }

        public void Update(Page item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Page item)
        {
            throw new NotImplementedException();
        }
    }
}
