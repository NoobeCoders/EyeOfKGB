using RestService.CSharp.Api.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.CSharp.Api.Admin.Controllers
{
    public class KeywordsController : ApiController
    {
        IKeywordsRepository repository = new FakeKeywordsRepository();
        // GET api/keywords
        public IEnumerable<Keyword> Get()
        {
            return repository.Get();
        }

        // GET api/keywords/5
        public IEnumerable<Keyword> Get(int id)
        {
            return repository.Get(id);
        }

        // POST api/keywords
        public Keyword Post(Keyword keyword)
        {
            return repository.Add(keyword);
        }

        // PUT api/keywords/5
        public bool Put(Keyword keyword)
        {
            return repository.Update(keyword);
        }

        // DELETE api/keywords/5
        public bool Delete(int id)
        {
            return repository.Delete(id);
        }
    }
}
