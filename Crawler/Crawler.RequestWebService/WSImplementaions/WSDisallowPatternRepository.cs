using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSDisallowPatternRepository : IDisallowPatternRepository
    {
        IDownloader downloader;

        public WSDisallowPatternRepository(IDownloader downloader)
        {
            this.downloader = downloader;
        }

        public void Set(IEnumerable<string> disallowPatterns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisallowPattern> GetBySiteId(int siteId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisallowPattern> GetAll()
        {
            throw new NotImplementedException();
        }

        public DisallowPattern GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(DisallowPattern item)
        {
            throw new NotImplementedException();
        }

        public void Update(DisallowPattern item)
        {
            throw new NotImplementedException();
        }

        public void Delete(DisallowPattern item)
        {
            throw new NotImplementedException();
        }
    }
}
