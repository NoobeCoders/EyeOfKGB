﻿using BusinessLogic.Interfaces;
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
        IDataManager dataManager;
        HtmlPageContentHandler htmlPageContentHandler;
        RobotsPageContentHandler robotsPageContentHandler;
        SitemapPageContentHandler sitemapPageContentHandler;

        public PageHandler(IDataManager dataManager, IDownloader downloader)
        {
            this.downloader = downloader;
            this.dataManager = dataManager;

            htmlPageContentHandler = new HtmlPageContentHandler(dataManager);
            robotsPageContentHandler = new RobotsPageContentHandler(dataManager);
            sitemapPageContentHandler = new SitemapPageContentHandler(dataManager);
        }

        public void HandlePage(Page page)
        {
            string content = DownloadPageContent(page);

            if (IsRobotsPage(page))
                robotsPageContentHandler.HandleContent(page, content);
            else if (IsSitemapPage(page))
                sitemapPageContentHandler.HandleContent(page, content);
            else
                htmlPageContentHandler.HandleContent(page, content);

            page.LastScanDate = DateTime.Now;
        }

        private string DownloadPageContent(Page page)
        {
            return downloader.Download("http://" + page.URL);
        }
        private bool IsRobotsPage(Page page)
        {
            return page.URL.Contains("robots.txt");
        }

        private bool IsSitemapPage(Page page)
        {
            return page.URL.Contains("sitemap.xml");
        }
    }
}
