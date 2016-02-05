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
        // GET: api/Keywords
        public IEnumerable<Keyword> Get()
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                return dataManager.Keywords.GetAll().ToList();
            }
        }

        // GET: api/Keywords/5
        public IEnumerable<Keyword> Get(int id)
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                return dataManager.Persons.GetById(id).Keywords.ToList();
            }
        }

        // POST: api/Keywords
        public void Post(Keyword keyword)
        {
        }

        // PUT: api/Keywords/5
        public void Put(Keyword keyword)
        {
            using (DataManager dataManager = new DataManager("PrimaryConnection"))
            {
                dataManager.Keywords.Update(keyword);
            }
        }

        // DELETE: api/Keywords/5
        public void Delete(Keyword keyword)
        {
        }
    }
}
