using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Dawnloader dawnloader = new Dawnloader();
            string text = dawnloader.Download(@"https://github.com/robots.txt");
            Console.WriteLine(text);
            Console.ReadKey();
        }
    }
}
