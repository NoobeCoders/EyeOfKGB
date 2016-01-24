﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rest_Service.CSharp.Domain.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime FoundDateTime { get; set; }
        public DateTime LastScanDate { get; set; }

        public int SiteId { get; set; }
        public virtual Site Site { get; set; }

        public virtual ICollection<PersonPageRank> PersonPageRanks { get; set; } = new List<PersonPageRank>();

    }
}
