﻿using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.PageParser;
using Crawler.DAL;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using Crawler.Engine;
using MySql.Data.Entity;
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
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

            IDataManager dataManager = new DataManager("PrimaryConnection");
            IDownloader downloader = new Downloader();

            // Для теста
            foreach (Person item in dataManager.Persons.GetAll().ToList())
            {
                Console.WriteLine(item.Name);
            }

            using (CrawlerEngine crawler = new CrawlerEngine(dataManager, downloader))
            {
                //crawler.Start();
            }
            Console.ReadKey();
        }
    }
}
