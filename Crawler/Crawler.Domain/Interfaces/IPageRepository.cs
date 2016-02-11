﻿using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IPageRepository : IRepository<Page>
    {
        IEnumerable<Page> GetPagesByFounDateTime(DateTime date);
        IEnumerable<Page> GetPagesByLastScanDate(DateTime? date);
        IEnumerable<Page> GetPagesBySiteId(int id);
        Task<IEnumerable<Page>> GetPagesBySiteId(int id, int pageAmount);
        bool IsNewUrl(string url);
        Task<bool> IsNewUrlAsync(string url);
        Task<Page> GetByIdAsync(int pageId);
        void Delete(int id);
    }
}
