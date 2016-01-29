using BusinessLogic.Interfaces;
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
        IDataManager dataManager;
        HtmlPageContentHandler htmlPageContentHandler;
        RobotsPageContentHandler robotsPageContentHandler;
        SitemapPageContentHandler sitemapPageContentHandler;

        public PageHandler(IDataManager dataManager, IDownloader downloader, IParser parser)
        {
            this.downloader = downloader;
            this.dataManager = dataManager;

            htmlPageContentHandler = new HtmlPageContentHandler(dataManager, parser);
            robotsPageContentHandler = new RobotsPageContentHandler(dataManager, parser);
            sitemapPageContentHandler = new SitemapPageContentHandler(dataManager, parser);
        }

        public async Task<int> HandlePage(Page page)
        {
            string content = await DownloadPageContent(page);

            if (IsRobotsPage(page))
                robotsPageContentHandler.HandleContent(page, content);
            else if (IsSitemapPage(page))
                sitemapPageContentHandler.HandleContent(page, content);
            else
                htmlPageContentHandler.HandleContent(page, content);

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
