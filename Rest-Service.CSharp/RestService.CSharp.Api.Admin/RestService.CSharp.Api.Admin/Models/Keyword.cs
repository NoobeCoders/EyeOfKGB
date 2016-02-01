using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService.CSharp.Api.Admin.Models
{
    public class Keyword
    {
        public Keyword() { }

        public Keyword(int keywordID, string keywordName, int personID)
        {
            KeywordID = keywordID;
            KeywordName = keywordName;
            PersonID = personID;
        }

        public int KeywordID { get;  set; }
        
        public string KeywordName { get; set; }

        public int PersonID { get; set; }
    }
}