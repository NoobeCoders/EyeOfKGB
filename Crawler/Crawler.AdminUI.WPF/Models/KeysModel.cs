using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.AdminUI.WPF.Models
{
    public class KeysModel : DependencyObject
    {
        public static DependencyProperty PeopleProperty;
        public static DependencyProperty KeysProperty;
        public static DependencyProperty SelectedKeyProperty;

        static KeysModel()
        {
            PeopleProperty = DependencyProperty.Register("People",
                typeof(List<string>),
                typeof(KeysModel),
                new PropertyMetadata(new List<string>() { "выберите личность " }));

            KeysProperty = DependencyProperty.Register("Keys",
                typeof(List<string>),
                typeof(KeysModel),
                new PropertyMetadata(new List<string>()));

            SelectedKeyProperty = DependencyProperty.Register("SelectedKey",
                typeof(string),
                typeof(KeysModel),
                new PropertyMetadata(string.Empty));
        }

        public List<string> People
        {
            get { return GetValue(PeopleProperty) as List<string>; }
            set { SetValue(PeopleProperty, value); }
        }

        public List<string> Keys
        {
            get { return GetValue(KeysProperty) as List<string>; }
            set { SetValue(KeysProperty, value); }
        }

        public string SelectedKey
        {
            get { return GetValue(SelectedKeyProperty) as string; }
            set { SetValue(SelectedKeyProperty, value); }
        }
    }
}
