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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Keywords/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Keywords
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Keywords/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Keywords/5
        public void Delete(int id)
        {
        }
    }
}
