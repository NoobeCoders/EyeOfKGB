using Rest_Service.CSharp.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rest_Service.CSharp.Domain.Entities;

namespace Rest_Service.CSharp.DAL.Repositories
{
    public class StatsRepositoryFake : IStatsRepository
    {
        public IEnumerable<PersonPageRank> GetStatByRangeDate(int siteId, int personId, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonPageRank> GetStatBySite(int id)
        {
            return fakePersonRank.Where(x => x.Page.SiteId == id);
        }

        public List<PersonPageRank> fakePersonRank { get; set; }
        public List<Person> fakePerson { get; set; }
        public List<Site> fakeSite { get; set; }
        public List<Page> fakePage { get; set; }

        public StatsRepositoryFake()
        {
            fakePage = new List<Page>()
            {
                new Page()
                {
                    Id = 1,
                    SiteId = 1,
                    LastScanDate = DateTime.Today
                },
                new Page()
                {
                    Id = 2,
                    SiteId = 2,
                    LastScanDate = DateTime.Today
                }
            };

            fakePerson = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Vova",
                    
                },
                new Person()
                {
                    Id = 2,
                    Name = "Misha",
                }
            };

            fakePersonRank = new List<PersonPageRank>()
            {
                new PersonPageRank()
                {
                    Person = fakePerson[0],
                    Page = fakePage[0],
                    Rank = 40

                },
                new PersonPageRank()
                {
                    Person = fakePerson[1],
                    Page = fakePage[1],
                    Rank = 20
                }
            };
        }
    }
}
