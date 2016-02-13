using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL
{
    public class EFDataManagerFactory : IDataManagerFactory
    {
        string connectionString;

        public EFDataManagerFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDataManager GetDataManager()
        {
            return new DataManager(connectionString);
        }
    }
}
