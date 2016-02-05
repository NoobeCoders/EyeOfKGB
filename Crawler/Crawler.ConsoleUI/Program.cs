using BusinessLogic;
using BusinessLogic.Models;
using Crawler.DAL;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using Crawler.Engine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            //foreach (Person item in dataManager.Persons.GetAll())
            //{
            //    Console.WriteLine(item.Name);
            //}

            using (CrawlerEngine crawler = new CrawlerEngine(dataManager, downloader))
            {
                crawler.Start().Wait();
            }
            Console.WriteLine("Сканирование закончено.");
            Console.ReadKey();
        }
    }
}
