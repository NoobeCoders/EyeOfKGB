using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.PageParser;
using Crawler.DAL;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using Crawler.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataManager dataManager = new DataManager("MSSQLConnection");
            IDownloader downloader = new Downloader();

            using (CrawlerEngine crawler = new CrawlerEngine(dataManager, downloader))
            {
                crawler.Start();
            }
            Console.ReadKey();
        }
    }
}
