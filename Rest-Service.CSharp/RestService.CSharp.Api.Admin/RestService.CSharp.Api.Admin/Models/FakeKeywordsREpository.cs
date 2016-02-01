using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.CSharp.Api.Admin.Models
{
    public class FakeKeywordsRepository : IKeywordsRepository
    {
        List<Keyword> keywords = new List<Keyword>()
                                                      {
                                                          new Keyword(1, "Путина", 1),
                                                          new Keyword(2, "Путиy", 1),
                                                          new Keyword(3, "Путиным", 1),
                                                          
                                                          new Keyword(4, "Медведева", 2),
                                                          new Keyword(5, "Медведеву", 2),
                                                          new Keyword(6, "Айфончик", 2),

                                                          new Keyword(7, "Навальному", 3),
                                                          new Keyword(8, "Навального", 3),
                                                          new Keyword(9, "Навальным", 3),
                                                      };

        public IEnumerable<Keyword> Get()
        {
            return keywords; 
        }

        public IEnumerable<Keyword> Get(int id)
        {
            return keywords.Where(k => k.PersonID == id);
        }

        public Keyword Add(Keyword item)
        {
            lock (keywords)
            {

                item.KeywordID = keywords.Count + 1;
                keywords.Add(item);
            }
            return item;
        }

        public bool Update(Keyword item)
        {
            lock (keywords)
            {
                Keyword storedKeyword = keywords.First(k=> k.KeywordID==item.KeywordID);
                if (storedKeyword != null)
                {
                    storedKeyword.KeywordName = item.KeywordName;
                    storedKeyword.PersonID = item.PersonID; 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Delete(int id)
        {
            lock (keywords)
            {
                Keyword storedKeyword = keywords.First(k=> k.KeywordID== id);
                if (storedKeyword != null)
                {
                    keywords.Remove(storedKeyword);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}