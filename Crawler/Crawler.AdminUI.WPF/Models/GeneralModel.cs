﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.AdminUI.WPF.Models
{
    public class GeneralModel : DependencyObject
    {
        public struct SiteRank
        {
            public string name;
            public int rank;
        }

        public static DependencyProperty SitesProperty;
        public static DependencyProperty SiteRanksProperty;
        public static DependencyProperty SelectedSiteProperty;

        static GeneralModel()
        {
            SitesProperty = DependencyProperty.Register("Sites",
                                                        typeof(List<string>),
                                                        typeof(GeneralModel),
                                                        new PropertyMetadata(new List<string>(){ "выберите сайт" }));

            SiteRanksProperty = DependencyProperty.Register("SiteRanks",
                                                        typeof(List<SiteRank>),
                                                        typeof(GeneralModel),
                                                        new PropertyMetadata(new List<SiteRank>()));

            SelectedSiteProperty = DependencyProperty.Register("SelectedSite",
                                                        typeof(string),
                                                        typeof(GeneralModel),
                                                        new PropertyMetadata("выберите сайт"));
        }

        public List<string> Sites
        {
            get { return GetValue(SitesProperty) as List<string>; }
            set { SetValue(SitesProperty, value); }
        }

        public List<SiteRank> SiteRanks
        {
            get { return GetValue(SiteRanksProperty) as List<SiteRank>; }
            set { SetValue(SiteRanksProperty, value); }
        }

        public string SelectedSite
        {
            get { return (string)GetValue(SelectedSiteProperty); }
            set { SetValue(SelectedSiteProperty, value); }
        }
    }
}
