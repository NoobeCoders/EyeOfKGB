using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.DBInitializers
{
    abstract public class InitializationDB
    {
        public abstract void Initialization(ApplicationDbContext context);
    }
}
