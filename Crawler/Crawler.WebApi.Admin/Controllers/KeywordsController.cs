using Crawler.DAL;
using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Crawler.WebApi.Admin.Controllers
{
    public class KeywordsController : ApiController
    {
        DataManager dataManager = new DataManager("PrimaryConnection");
        // GET: api/Keywords
        public IEnumerable<Keyword> Get()
        {            
            return dataManager.Keywords.GetAll().ToList();            
        }

        // GET: api/Keywords/5
        public IEnumerable<Keyword> Get(int id)
        {
            return dataManager.Persons.GetById(id).Keywords.ToList();
        }

        // POST: api/Keywords
        public void Post(Keyword keyword)
        {
            dataManager.Keywords.Add(keyword);
        }

        // PUT: api/Keywords/5
        public void Put(Keyword keyword)
        {
            dataManager.Keywords.Update(keyword);
        }

        // DELETE: api/Keywords/5
        public void Delete(int id)
        {
            dataManager.Keywords.Update(id);
        }
    }
}
