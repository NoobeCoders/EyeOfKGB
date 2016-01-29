using Crawler.Domain.Entities;
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
        List<DisallowPattern> disallowPatterns { get; set; }

        public void Add(DisallowPattern item)
        {
            disallowPatterns.Add(item);
        }

        public void Delete(DisallowPattern item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisallowPattern> GetAll()
        {
            return disallowPatterns;
        }

        public string GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(IEnumerable<string> disallowPatterns)
        {
            throw new NotImplementedException();
        }

        public void Update(DisallowPattern item)
        {
            throw new NotImplementedException();
        }

        DisallowPattern IRepository<DisallowPattern>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
