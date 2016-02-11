using BusinessLogic.Interfaces;
using Crawler.DAL;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    class PageHandler
    {
        IDownloader downloader;
        IParser parser;

        public PageHandler(IDataManager dataManager, IDownloader downloader, IParser parser, Site site)
        {
            this.downloader = downloader;
            this.parser = parser;
        }

        public async Task<int> HandlePage(Page page)
        {
            string content = await DownloadPageContent(page);

            using (DataManager dataManager = new DataManager("MSSQLConnection"))
            {
                if (IsRobotsPage(page))
                    await new RobotsPageContentHandler(dataManager, parser, page.Site).HandleContent(page.Id, content);
                else if (IsSitemapPage(page))
                    await new SitemapPageContentHandler(dataManager, parser, page.Site).HandleContent(page.Id, content);
                else
                    await new HtmlPageContentHandler(dataManager, parser, page.Site).HandleContent(page.Id, content);

                await dataManager.Save();
            }

            page.LastScanDate = DateTime.Now;

            return page.Id;
        }

        private async Task<string> DownloadPageContent(Page page)
        {
            return await downloader.Download(page.URL);
        }
        private bool IsRobotsPage(Page page)
        {
            return page.URL.Contains("robots.txt");
        }

        private bool IsSitemapPage(Page page)
        {
            return page.URL.Contains("sitemap") && page.URL.Contains(".xml");
        }
    }
}
