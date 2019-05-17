using System;
using System.Collections;
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
    /// Interaction logic for UserControl4.xaml
    /// </summary>
    public partial class UserControl4 : UserControl
    {
        private Data tempData = new Data();
        private ArrayList arrayList = new ArrayList();
        private ArrayList deletedRows = new ArrayList();
        private SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
        public UserControl4()
        {
            sqlConnection.Open();
            InitializeComponent();
            DataContext = UsersCollection;
            InitData();
        }
        private void InitData()
        {
            InitUsersCollection();
            SqlDataAdapter sqlDataUnitsAdapter = new SqlDataAdapter("select * from c_units", sqlConnection);
            DataTable dataTable1 = new DataTable();
            sqlDataUnitsAdapter.Fill(dataTable1);
            UnitColumn.ItemsSource = dataTable1.DefaultView;
            UnitColumn.DisplayMemberPath = dataTable1.Columns["ad"].ToString();
            UnitColumn.SelectedValuePath = dataTable1.Columns["id"].ToString();
            UnitColumn.SelectedValueBinding = new Binding("Unit") { Mode = BindingMode.TwoWay, NotifyOnSourceUpdated = true };
            SqlDataAdapter sqlDataGroupsAdapter = new SqlDataAdapter("select * from c_qruplar", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataGroupsAdapter.Fill(dataTable);
            GroupColumn.ItemsSource = dataTable.DefaultView;
            GroupColumn.DisplayMemberPath = dataTable.Columns["ad"].ToString();
            GroupColumn.SelectedValuePath = dataTable.Columns["id"].ToString();
            GroupColumn.SelectedValueBinding = new Binding("Group") { Mode = BindingMode.TwoWay, NotifyOnSourceUpdated = true };
            //SqlDataAdapter sqlDataProvidersAdapter = new SqlDataAdapter("select * from c_mallar", sqlConnection);
            //DataTable dataTable = new DataTable("myData");
            //sqlDataProvidersAdapter.Fill(dataTable);
            dataGrid1ListOfGoods.ItemsSource = UsersCollection;
            dataGrid1ListOfGoods.PreviewKeyDown += DataGrid1ListOfGoods_KeyDown;
        }

        private void DataGrid1ListOfGoods_KeyDown(object sender, KeyEventArgs e)
        {
            //((DataGridRow)(sender)).Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
            if ((e.OriginalSource.GetType() == typeof(DataGridCell))&&(e.Key == Key.Delete))
            {
                MessageBoxResult result = MessageBox.Show("Are you sure want to delete this record", "Delete Record", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        int selectedGoodId = UsersCollection.ElementAt(((DataGrid)sender).SelectedIndex).Id;
                        new SqlCommand("update c_mallar set is_active = 1 where id = " + selectedGoodId, sqlConnection).ExecuteNonQuery();
                        MainWindow.shouldUpdateGoodsList = true;
                        UpdateGoodsList();
                        var row = (DataGridRow)(((DataGrid)sender).ItemContainerGenerator.ContainerFromIndex(((DataGrid)sender).SelectedIndex));
                        row.Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
                        break;
                    case MessageBoxResult.No:
                        e.Handled = true;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Handled = true;
                        break;
                }
            }
        }
        private void UpdateGoodsList()
        {
            SqlDataAdapter sqlDataProductsAdapter = new SqlDataAdapter("select ad,id from c_mallar where is_active != 1", sqlConnection);
            DataTable dataTable2 = new DataTable();
            sqlDataProductsAdapter.Fill(dataTable2);
            TabControl tabControl = (TabControl)((TabItem)(this.Parent)).Parent;
            int count = 0;
            foreach (TabItem tabItem in tabControl.Items)
            {
                if (tabItem.Content.GetType() == typeof(UserControl1))
                {
                    count++;
                    UserControl1 userControl1 = (UserControl1) tabItem.Content;
                    //int childrenCount = VisualTreeHelper.GetChildrenCount(userControl1);
                    DataGrid dataGrid = (DataGrid)LogicalTreeHelper.FindLogicalNode(userControl1, "dataGrid1");
                    DataGridComboBoxColumn GoodsBoxColumn = (DataGridComboBoxColumn) dataGrid.Columns[0];
                    GoodsBoxColumn.ItemsSource = dataTable2.DefaultView;
                    GoodsBoxColumn.DisplayMemberPath = dataTable2.Columns["ad"].ToString();
                    GoodsBoxColumn.SelectedValuePath = dataTable2.Columns["id"].ToString();
                    String header = dataGrid.Columns[0].Header.ToString();
                }
            }
        }

        private void dataGrid1_UnloadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void dataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.GetIndex() < UsersCollection.Count)
            {
                if (UsersCollection.ElementAt(e.Row.GetIndex()).IsActive == 1)
                    e.Row.Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
            }
        }
        public ObservableCollection<Data> UsersCollection { get; set; }
        public class Data
        {
            public int IsActive { get; set; }
            public string Artikul { get; set; }
            public string Ad { get; set; }
            public int Unit { get; set; }
            public int Group { get; set; }
            public int Id { get; set; }
            public string Comments { get; set; }
        }
        private void InitUsersCollection()
        {
            UsersCollection = new ObservableCollection<Data>();
            SqlDataAdapter sqlDataProvidersAdapter = new SqlDataAdapter("select * from c_mallar", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataProvidersAdapter.Fill(dataTable);
            for (int i = 0; i < dataTable.Rows.Count; i++)
                UsersCollection.Add(new Data
                {
                    Comments = dataTable.Rows[i]["comments"].ToString(),
                    IsActive = int.Parse(dataTable.Rows[i]["is_active"].ToString()),
                    Artikul = dataTable.Rows[i]["artikul"].ToString(),
                    Ad = dataTable.Rows[i]["ad"].ToString(),
                    Id = int.Parse(dataTable.Rows[i]["id"].ToString()),
                    Unit = int.Parse(dataTable.Rows[i]["unit_id"].ToString()),
                    Group = int.Parse(dataTable.Rows[i]["qrup_id"].ToString())
                });
        }

        private void DataGrid1ListOfGoods_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            int index = ((DataGrid)sender).SelectedIndex;
            if (UsersCollection.ElementAt(index) != tempData)
                if (!arrayList.Contains(tempData.Id))
                    arrayList.Add(tempData.Id);
        }

        private void DataGrid1ListOfGoods_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int index = 0;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                index = e.Row.GetIndex();
                tempData.IsActive = UsersCollection.ElementAt(index).IsActive;
                tempData.Id = UsersCollection.ElementAt(index).Id;
                tempData.Artikul = UsersCollection.ElementAt(index: index).Artikul;
                tempData.Ad = UsersCollection.ElementAt(index).Ad;
                tempData.Unit = UsersCollection.ElementAt(index).Unit;
                tempData.Group = UsersCollection.ElementAt(index).Group;
                tempData.Comments = UsersCollection.ElementAt(index).Comments;
            }
        }
    }
}
