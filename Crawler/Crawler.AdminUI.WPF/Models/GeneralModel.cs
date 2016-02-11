using System;
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

        public static DependencyProperty SitesGeneralProperty;
        public static DependencyProperty SiteRanksProperty;
        public static DependencyProperty SelectedSiteProperty;

        static GeneralModel()
        {
            SitesGeneralProperty = DependencyProperty.Register("SitesGeneral",
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

        public List<string> SitesGeneral
        {
            get { return GetValue(SitesGeneralProperty) as List<string>; }
            set { SetValue(SitesGeneralProperty, value); }
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
