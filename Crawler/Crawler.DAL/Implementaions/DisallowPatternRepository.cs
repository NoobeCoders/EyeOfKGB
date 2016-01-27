using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.Implementaions
{
    class DisallowPatternRepository : IDisallowPatternRepository
    {
        IEnumerable<String> disallowPatterns { get; set; }

        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAll()
        {
            return disallowPatterns;
        }

        public string GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(IEnumerable<string> disallowPatterns)
        {
            this.disallowPatterns = disallowPatterns;
        }

        public void Update(string item)
        {
            throw new NotImplementedException();
        }
    }
}
