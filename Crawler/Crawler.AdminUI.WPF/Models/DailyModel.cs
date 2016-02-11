using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.AdminUI.WPF.Models
{
    public class DailyModel : DependencyObject
    {
        public struct DailyRank
        {
            public DateTime date;
            public int rank;
        }

        public static DependencyProperty SitesDailyProperty;
        public static DependencyProperty PeopleDailyProperty;
        public static DependencyProperty DateBeginProperty;
        public static DependencyProperty DateEndProperty;
        public static DependencyProperty DailyRanksProperty;

        static DailyModel()
        {
            SitesDailyProperty = DependencyProperty.Register("SitesDaily",
                                                        typeof(List<string>),
                                                        typeof(GeneralModel),
                                                        new PropertyMetadata(new List<string>(){ "выберите сайт" }));

            PeopleDailyProperty = DependencyProperty.Register("PeopleDaily",
                                                        typeof(List<string>),
                                                        typeof(GeneralModel),
                                                        new PropertyMetadata(new List<string>(){ "выберите личность" }));

            DateBeginProperty = DependencyProperty.Register("DateBegin",
                                                        typeof(DateTime),
                                                        typeof(DailyModel),
                                                        new PropertyMetadata(DateTime.Now));

            DateEndProperty = DependencyProperty.Register("DateEnd",
                                                        typeof(DateTime),
                                                        typeof(DailyModel),
                                                        new PropertyMetadata(DateTime.Now));

            DailyRanksProperty = DependencyProperty.Register("DailyRanks",
                                                        typeof(List<DailyRank>),
                                                        typeof(DailyModel),
                                                        new PropertyMetadata(new List<DailyRank>()));
        }

        public List<string> SitesDaily
        {
            get { return GetValue(SitesDailyProperty) as List<string>; }
            set { SetValue(SitesDailyProperty, value); }
        }
        public List<string> PeopleDaily
        {
            get { return GetValue(PeopleDailyProperty) as List<string>; }
            set { SetValue(PeopleDailyProperty, value); }
        }

        public DateTime DateBegin
        {
            get { return (DateTime)GetValue(DateBeginProperty); }
            set { SetValue(DateBeginProperty, value); }
        }

        public DateTime DateEnd
        {
            get { return (DateTime)GetValue(DateEndProperty); }
            set { SetValue(DateEndProperty, value); }
        }

        public List<DailyRank> DailyRanks
        {
            get { return GetValue(DailyRanksProperty) as List<DailyRank>; }
            set { SetValue(DailyRanksProperty, value); }
        }

    }
}
