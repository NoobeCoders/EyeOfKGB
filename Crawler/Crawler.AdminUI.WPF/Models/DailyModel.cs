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

        public static DependencyProperty SitesProperty;
        public static DependencyProperty PeopleProperty;
        public static DependencyProperty DateBeginProperty;
        public static DependencyProperty DateEndProperty;
        public static DependencyProperty DailyRanksProperty;

        static DailyModel()
        {
            SitesProperty = DependencyProperty.Register("Sites",
                                                        typeof(List<string>),
                                                        typeof(GeneralModel),
                                                        new PropertyMetadata(new List<string>(){ "выберите сайт" }));

            PeopleProperty = DependencyProperty.Register("People",
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

        public List<string> Sites
        {
            get { return GetValue(SitesProperty) as List<string>; }
            set { SetValue(SitesProperty, value); }
        }
        public List<string> People
        {
            get { return GetValue(PeopleProperty) as List<string>; }
            set { SetValue(PeopleProperty, value); }
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
