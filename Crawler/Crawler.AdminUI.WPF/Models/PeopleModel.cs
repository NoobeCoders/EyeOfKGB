using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.AdminUI.WPF.Models
{
    public class PeopleModel : DependencyObject
    {
        public static DependencyProperty PeopleProperty;
        public static DependencyProperty SelectedPersonProperty;

        static PeopleModel()
        {
            PeopleProperty = DependencyProperty.Register("People",
                                                        typeof(List<string>),
                                                        typeof(KeysModel),
                                                        new PropertyMetadata(new List<string>() { "выберите личность " }));

            SelectedPersonProperty = DependencyProperty.Register("SelectedPerson",
                                                        typeof(string),
                                                        typeof(KeysModel),
                                                        new PropertyMetadata(string.Empty));
        }

        public List<string> People
        {
            get { return GetValue(PeopleProperty) as List<string>; }
            set { SetValue(PeopleProperty, value); }
        }

        public string SelectedPerson
        {
            get { return GetValue(SelectedPersonProperty) as string; }
            set { SetValue(SelectedPersonProperty, value); }
        }
    }
}
