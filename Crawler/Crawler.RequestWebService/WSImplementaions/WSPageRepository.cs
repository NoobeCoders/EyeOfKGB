using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSPageRepository : IPageRepository
    {
        IDownloader downloader;

        public WSPageRepository(IDownloader downloader)
        {
            this.downloader = downloader;
        }

        public IEnumerable<Page> GetPagesByFounDateTime(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetPagesByLastScanDate(DateTime? date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetPagesBySiteId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Page>> GetPagesBySiteId(int id, int pageAmount)
        {
            throw new NotImplementedException();
        }

        public bool IsNewUrl(string url)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNewUrlAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<Page> GetByIdAsync(int pageId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
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
