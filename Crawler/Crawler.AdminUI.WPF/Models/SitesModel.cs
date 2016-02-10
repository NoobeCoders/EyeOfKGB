using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.AdminUI.WPF.Models
{
    public class SitesModel : DependencyObject
    {
        public static DependencyProperty SitesProperty;
        public static DependencyProperty SelectedSiteProperty;

        static SitesModel()
        {
            SitesProperty = DependencyProperty.Register("Sites",
                                                        typeof(List<string>),
                                                        typeof(SitesModel),
                                                        new PropertyMetadata(new List<string>()));

            SelectedSiteProperty = DependencyProperty.Register("SelectedSite",
                                                        typeof(string),
                                                        typeof(SitesModel),
                                                        new PropertyMetadata(string.Empty));
        }

        public List<string> Sites
        {
            get { return GetValue(SitesProperty) as List<string>; }
            set { SetValue(SitesProperty, value); }
        }

        public string SelectedSite
        {
            get { return GetValue(SelectedSiteProperty) as string; }
            set { SetValue(SelectedSiteProperty, value); }
        }
    }
}
