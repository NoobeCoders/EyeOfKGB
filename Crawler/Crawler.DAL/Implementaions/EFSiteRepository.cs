﻿using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.Implementaions
{
    public class EFSiteRepository : ISiteRepository
    {
        ApplicationDbContext dbContext;

        public EFSiteRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Site GetSiteByName(string name)
        {
            return dbContext.Sites.FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Site> GetSitesWithoutPages()
        {
            return dbContext.Sites.Where(s => s.Pages.Count == 0).ToList();
        }

        public IEnumerable<Site> GetAll()
        {
            return dbContext.Sites;
        }

        public Site GetById(int id)
        {
            return dbContext.Sites.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Site item)
        {
            dbContext.Sites.Add(item);
        }

        public void Update(Site item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
        
        public void Delete(Site item)
        {
            Delete(item.Id);
        }


        public void Delete(int id)
        {
            Site site = GetById(id);
            if (site != null)
                dbContext.Sites.Remove(site);
        }
    }
}
