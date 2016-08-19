using Crawler.DAL.DBInitializers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Здесь инициализируем БД

            DBInit init = new DBInit(context);

            // Здесь добавляем созданные нами объекты, наследованные от InitializationDB, для инициализации БД
            init.Add(new InitPersons());
            init.Add(new InitSites());

            init.Initialization();
        }
    }
}
