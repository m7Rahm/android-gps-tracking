using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UserControl3.xaml
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        private Data tempData = new Data();
        private ArrayList arrayList = new ArrayList();
        private ArrayList deletedRows = new ArrayList();
        private SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
        private int qaime_nomre;
        public UserControl3(int qaime_nomre)
        {
            sqlConnection.Open();
            this.qaime_nomre = qaime_nomre;
            InitializeComponent();
            InitData();
            //TabControl tabControl = (TabControl)((TabItem)(this.Parent)).Parent;
            //new Thread(UpdateData).Start();

        }
        private void UpdateData()
        {
            while (true)
            {
                if (MainWindow.shouldUpdateQaimelersList)
                {
                    this.Dispatcher.Invoke(() => UpdateData());
                    MainWindow.shouldUpdateQaimelersList = false;
                }
            }
        }

        private void InitData()
        {
            InitUsersCollection();
            DataContext = UsersCollection;
            SqlDataAdapter sqlDataGoodsAdapter = new SqlDataAdapter("select * from c_mallar", sqlConnection);
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataUnitsAdapter = new SqlDataAdapter("select * from c_units", sqlConnection);
            DataTable dataTable1 = new DataTable();
            sqlDataGoodsAdapter.Fill(dataTable);
            SqlDataAdapter sqlDataProvidersAdapter = new SqlDataAdapter("select * from c_malsatanlar", sqlConnection);
            sqlDataUnitsAdapter.Fill(dataTable1);
            DataTable dataTable2 = new DataTable();
            sqlDataProvidersAdapter.Fill(dataTable2);
            dataGrid1.ItemsSource = UsersCollection;
            //DataTable dataTable = new SqlDa(sqlConnection).GetProductsDataTable();
            //DataTable dataTable3 = new SqlData(sqlConnection).GetProductsDataTable();
            ComboBoxColumn.ItemsSource = dataTable.DefaultView;
            ComboBoxColumn.DisplayMemberPath = dataTable.Columns["ad"].ToString();
            ComboBoxColumn.SelectedValuePath = dataTable.Columns["id"].ToString();
            UnitColumn.ItemsSource = dataTable1.DefaultView;
            UnitColumn.DisplayMemberPath = dataTable1.Columns["ad"].ToString();
            UnitColumn.SelectedValuePath = dataTable1.Columns["id"].ToString();
            comboBox.ItemsSource = dataTable2.DefaultView;
            comboBox.DisplayMemberPath = dataTable2.Columns["ad"].ToString();
            comboBox.SelectedValuePath = dataTable2.Columns["id"].ToString();

        }

        private void InitUsersCollection()
        {
            UsersCollection = new ObservableCollection<Data>();
            DataTable dataTable = UpdateSourceData();
            for (int i = 0; i < dataTable.Rows.Count; i++)
                UsersCollection.Add(new Data
                {
                    Id = int.Parse(dataTable.Rows[i]["id"].ToString()),
                    Miqdar = float.Parse(dataTable.Rows[i]["miqdar"].ToString()),
                    Qiymet = float.Parse(dataTable.Rows[i]["qiymet"].ToString()),
                    Mebleq = float.Parse(dataTable.Rows[i]["mebleq"].ToString()),
                    ProductData = dataTable.Rows[i]["mal_id"].ToString(),
                    IsProductActive = int.Parse(dataTable.Rows[i]["is_active"].ToString()),
                    UnitData = int.Parse(dataTable.Rows[i]["vahid_id"].ToString()),
                    MalsatanID = int.Parse(dataTable.Rows[i]["malsatan_id"].ToString()),
                    Qaime_N = int.Parse(dataTable.Rows[i]["qaime_N"].ToString()),
                    Vaxt = dataTable.Rows[i]["vaxt"].ToString(),
                });
        }

        private DataTable UpdateSourceData()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from [RestaurantDB].[dbo].[list_details_view] where qaime_N = " + qaime_nomre, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public ObservableCollection<Data> UsersCollection { get; set; }
        public class Data
        {
            public int Id { get; set; }
            public float Miqdar { get; set; }
            public float Qiymet { get; set; }
            public float Mebleq { get; set; }
            public string ProductData { get; set; }
            public int IsProductActive { get; set; }
            public int UnitData { get; set; }
            public int MalsatanID { get; set; }
            public int Qaime_N { get; set; }
            public string Vaxt { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            ObservableCollection<Data> datas = UsersCollection;
            int qaimeN;
            if (qaime_N.Text != null && !qaime_N.Text.Equals(""))
                qaimeN = Convert.ToInt32(qaime_N.Text.ToString());
            else
            {
                MessageBox.Show("Fill in Qaimə № field");
                return;
            }
            String date;
            if (datePicker.SelectedDate != null)
                date = datePicker.SelectedDate.Value.Date.ToShortDateString();
            else
            {
                MessageBox.Show("Fill in time field");
                return;
            }
            String updateCommmandString = "";
            String insertCommandString = "";
            for (int i = 0; i < datas.Count; i++)
            {
                foreach (int id in arrayList)
                {
                    if (datas[i].Id == id && id !=0)
                    {
                        updateCommmandString = "exec update_records_proc @id = " + id + " ,@qiymet =" + datas[i].Qiymet + ",@miqdar =" + datas[i].Miqdar + ",@mebleq ="
                      + datas[i].Mebleq + ",@malsatan_id =" + comboBox.SelectedValue + ",@vahid_id =" + datas[i].UnitData + ",@tarix = '" + date +
                      "',@qaime_N =" + qaimeN + ",@mal_id =" + datas[i].ProductData;
                        SqlCommand updateSqlCommand = new SqlCommand(updateCommmandString, sqlConnection);
                        updateSqlCommand.ExecuteNonQuery();
                    }
                    else continue;
                }
                if (datas[i].Id == 0)
                {
                    insertCommandString = "exec insert_new_record @qiymet =" + datas[i].Qiymet + ",@miqdar =" + datas[i].Miqdar + ",@mebleq =" + datas[i].Mebleq + ",@malsatan_id =" + comboBox.SelectedValue + 
                        ",@vahid_id =" + datas[i].UnitData + ",@tarix = '" + date + "',@qaime_N =" + qaimeN + ",@mal_id =" + datas[i].ProductData;
                    SqlCommand insertSqlCommand = new SqlCommand(insertCommandString, sqlConnection);
                    insertSqlCommand.ExecuteNonQuery();
                    datas[i].Id = int.Parse(UpdateSourceData().Rows[i]["id"].ToString());
                }
            }
            foreach (int id in deletedRows)
            {
                    SqlCommand sqlDeleteCommand = new SqlCommand("delete from insert_record where qaime_N = " + qaimeN + " and id = " + id, sqlConnection);
                    sqlDeleteCommand.ExecuteNonQuery();
            }
            TabControl tabControl = (TabControl)((TabItem)((UserControl3)(dockP.Parent)).Parent).Parent;
            foreach (TabItem tabItem in tabControl.Items)
            {
                if (tabItem.Content.GetType() == typeof(UserControl2))
                {
                    UserControl2 userControl2 = (UserControl2)tabItem.Content;
                    DataGrid dataGrid = (DataGrid)userControl2.FindName("dataGrid1ListOfImports");
                    userControl2.InitData("");
                }
                else continue;
            }

        }

        private void dataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.GetIndex() < UsersCollection.Count)
            {
                UsersCollection.ElementAt(e.Row.GetIndex()).Qaime_N = qaime_nomre;
                qaime_N.Text = qaime_nomre.ToString();
                if (UsersCollection.ElementAt(e.Row.GetIndex()).IsProductActive == 1)
                    e.Row.Background = new SolidColorBrush(Colors.GreenYellow);
            }
        }

        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int index = 0;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                index = e.Row.GetIndex();
                tempData.ProductData = UsersCollection.ElementAt(index).ProductData;
                tempData.Id = UsersCollection.ElementAt(index).Id;
                tempData.Mebleq = UsersCollection.ElementAt(index: index).Mebleq;
                tempData.MalsatanID = UsersCollection.ElementAt(index).MalsatanID;
                tempData.Miqdar = UsersCollection.ElementAt(index).Miqdar;
                tempData.Qiymet = UsersCollection.ElementAt(index).Qiymet;
                tempData.UnitData = UsersCollection.ElementAt(index).UnitData;
                tempData.Vaxt = UsersCollection.ElementAt(index).Vaxt;
            }
        }

        private void dataGrid1_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            int index = ((DataGrid)sender).SelectedIndex;
            if (UsersCollection.ElementAt(index) != tempData)
                if (!arrayList.Contains(tempData.Id))
                    arrayList.Add(tempData.Id);
        }

        private void dataGrid1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.OriginalSource.GetType()==typeof(DataGridCell))&&(e.Key == Key.Delete))
            {
                deletedRows.Add(UsersCollection.ElementAt(((DataGrid)sender).SelectedIndex).Id);
            }
        }
    }
}