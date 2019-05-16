using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl7.xaml
    /// </summary>
    public partial class UserControl7 : UserControl
    {
        private static UserControl7 userControl7 = new UserControl7();
        public static readonly DependencyProperty IdProperty = DependencyProperty.Register
            (
                 "Id",
                 typeof(string),
                 typeof(UserControl7),
                 new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnDependencyChanged)
            ));
        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value.ToString()); }
        }
        public UserControl7()
        {
            InitializeComponent();
        }
        private static void OnDependencyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //String str = e.NewValue.ToString();
            userControl7.Id = str;
        }
    }
}
