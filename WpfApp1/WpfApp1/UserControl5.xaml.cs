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
    /// Interaction logic for UserControl5.xaml
    /// </summary>
    public partial class UserControl5 : UserControl
    {
        private SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
        public UserControl5()
        {
            InitializeComponent();
            InitData();
            DataContext = UsersCollection;
            //foreach (DataGridColumn dataGridColumn in dtaGridListOfProviders.Columns)
            //dataGridColumn.IsReadOnly = false;
        }

        private void InitData()
        {
            UsersCollection = new ObservableCollection<Data>();
            DataTable dataTable = new DataTable("ProvidersTable");
            new SqlDataAdapter("SELECT * FROM [RestaurantDB].[dbo].[c_malsatanlar]", sqlConnection).Fill(dataTable);
            DataGridListOfProviders.ItemsSource = UsersCollection;
            for (int i = 0; i < dataTable.Rows.Count; i++)
                UsersCollection.Add(new Data
                {
                    Comments = dataTable.Rows[i]["comments"].ToString(),
                    //IsActive = int.Parse(dataTable.Rows[i]["is_active"].ToString()),
                    Ad = dataTable.Rows[i]["ad"].ToString(),
                    Id = int.Parse(dataTable.Rows[i]["id"].ToString()),
                    Telefon = dataTable.Rows[i]["telefon_nomresi"].ToString(),
                    Company = dataTable.Rows[i]["shirket"].ToString()
                });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public ObservableCollection<Data> UsersCollection { get; set; }
        public class Data
        {
            public int IsActive { get; set; }
            public string Ad { get; set; }
            public string Telefon { get; set; }
            public int Id { get; set; }
            public string Comments { get; set; }
            public string Company { get; set; }
        }
    }
}
