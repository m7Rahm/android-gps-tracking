using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for UserControl6.xaml
    /// </summary>
    public partial class UserControl6 : UserControl
    {
        private SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
        public UserControl6()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            UsersCollection = new ObservableCollection<Data>();
            InitUsersCollection();
            dataGridLeftovers.DataContext = UsersCollection;
            dataGridLeftovers.ItemsSource = UsersCollection;
            MalColumn.Binding = new Binding("Ad") { Mode = BindingMode.TwoWay };
            ArtikulColumn.Binding = new Binding("Artikul") { Mode = BindingMode.TwoWay };
            QaliqColumn.Binding = new Binding("Miqdar") { Mode = BindingMode.TwoWay };
            DeyerColumn.Binding = new Binding("Deyer") { Mode = BindingMode.TwoWay };
        }

        private void InitUsersCollection()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from anbar_qaliqi", sqlConnection);
            DataTable dataTable = new DataTable("myDataTable");
            sqlDataAdapter.Fill(dataTable: dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                UsersCollection.Add(new Data {
                    Ad = dataTable.Rows[i]["ad"].ToString(),
                    Miqdar = float.Parse(dataTable.Rows[i]["umumi_miqdar"].ToString()),
                    Artikul = int.Parse(dataTable.Rows[i]["artikul"].ToString()),
                    Deyer = float.Parse(dataTable.Rows[i]["deyer"].ToString()),
                    Id = int.Parse(dataTable.Rows[i]["id"].ToString())
                });
            }
        }

        public class Data
        {
            public int Artikul { get; set; }
            public float Miqdar { get; set; }
            public float Deyer { get; set; }
            public string Ad { get; set; }
            public int Id { get; set; }
            public int IsVisible { get; set; }
        }
        public ObservableCollection<Data> UsersCollection { get; set; }

        private Image image = new Image();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rowIndex = dataGridLeftovers.SelectedIndex;
            var row = (DataGridRow)dataGridLeftovers.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if ((rowIndex < UsersCollection.Count) && (UsersCollection.ElementAt(rowIndex).IsVisible != 0))
            {
                row.DetailsVisibility = Visibility.Collapsed;
                UsersCollection.ElementAt(rowIndex).IsVisible = 0;
                bool hasContent = dataGridLeftovers.RowDetailsTemplate.HasContent;
                //image.Source = new BitmapImage(new Uri(@"/WpfApp1;component/Resources/downArrow.png", UriKind.Relative));
                //((Button)sender).Content = image;
            }
            else if ((rowIndex < UsersCollection.Count)&& (UsersCollection.ElementAt(rowIndex).IsVisible == 0))
            {
                //dataGridLeftovers.RowDetailsTemplate = new UserControl7(6);
                row.DetailsVisibility = Visibility.Visible;
                UsersCollection.ElementAt(rowIndex).IsVisible = 1;
                //image.Source = new BitmapImage(new Uri(@"/WpfApp1;component/Resources/upArrow.png", UriKind.Relative));
                //((Button)sender).Content = image;
            }
            else
            {
                //image.Source = new BitmapImage(new Uri(@"/WpfApp1;component/Resources/downArrow.png", UriKind.Relative));
                //((Button)sender).Content = image;
            }
        }

    }
}
