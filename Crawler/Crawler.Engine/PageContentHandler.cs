using BusinessLogic.Interfaces;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    abstract class PageContentHandler
    {
        protected IDataManager dataManager;
        protected IParser parser;

        public PageContentHandler(IDataManager dataManager, IParser parser)
        {
            this.dataManager = dataManager;
            this.parser = parser;
        }

        public abstract void HandleContent(Page page, string content);
    }
}
