using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    class PageHandler
    {
        IDownloader downloader;

        public PageHandler(IDownloader downloader)
        {
            this.downloader = downloader;
        }

        public void HandlePage(Page page, PageContentHandler contentHandler)
        {
            string content = DownloadPageContent(page);

            contentHandler.HandleContent(page, content);
        }

        private string DownloadPageContent(Page page)
        {
            return downloader.Download("http://" + page.URL);
        }
    }
}
