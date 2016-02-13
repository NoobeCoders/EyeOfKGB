using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL
{
    public class EFDataManagerFabric : IDataManagerFabric
    {
        string connectionString;

        public EFDataManagerFabric(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDataManager GetDataManager()
        {
            return new DataManager(connectionString);
        }
    }
}
