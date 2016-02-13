namespace Crawler.DAL.Migrations
{
    using Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Crawler.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Crawler.DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Person poly = new Person()
            {
                Name = "Полиморфизм",
                Keywords = new List<Keyword>()
                {
                    new Keyword() { Name = "полиморфизм" },
                    new Keyword() { Name = "полиморфизму" },
                    new Keyword() { Name = "полиморфизма" }
                }
            };

            Person putin = new Person()
            {
                Name = "Путин Владимир Владимирович",
                Keywords = new List<Keyword>()
                {
                    new Keyword() { Name = "Путин" },
                    new Keyword() { Name = "Путину" },
                    new Keyword() { Name = "Путина" }
                }
            };

            context.Persons.Add(putin);
            context.Persons.Add(poly);

            Site geekbrains = new Site()
            {
                Name = "geekbrains.ru"
            };

            Site professorweb = new Site()
            {
                Name = "professorweb.ru"
            };

            Site lentaRu = new Site()
            {
                Name = "lenta.ru"
            };

            Site metanit = new Site()
            {
                Name = "metanit.com"
            };

            Site oper = new Site()
            {
                Name = "oper.ru"
            };

            //Site localhost = new Site()
            //{
            //    Name = "localhost",
            //    Pages = new List<Page>()
            //};

            //context.Sites.Add(lentaRu);
            context.Sites.Add(oper);
            context.Sites.Add(geekbrains);
            context.Sites.Add(metanit);
            context.Sites.Add(professorweb);
            //context.Sites.Add(geekBrains);

            //context.Sites.Add(localhost);

            context.SaveChanges();
        }
    }
}
