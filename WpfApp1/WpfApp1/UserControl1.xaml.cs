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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        //private float editingValueMiqdar = 0, editingValueQiymet = 0;
        //private float [] editingValuesMiqdarArray = new float[]
        private List<float> editingValuesMiqdarArray = new List<float>();
        private List<float> editingValuesQiymetArray = new List<float>();
        private bool isQaimeNumbOk = false;
        private int myQaimeNumb = -1;
        private bool hasSaveButtonBeenPreviouslyPressed = false;
        private bool hasDateBeenUpdated = false;
        private Data tempData = new Data();
        private ArrayList arrayList = new ArrayList();
        public TimeAndSeller timeAndSeller;
        private SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
        public UserControl1()
        {          
            //for (int i = 0; i < dataTable.Rows.Count; i++)
                //unitKeyPairs.Add(Convert.ToInt32(dataTable.Rows[i]["id"].ToString()), dataTable.Rows[i]["ad"].ToString());
            InitializeComponent();
            InitData();
            //DataTable data = new ProductsList(sqlConnection);
            //ComboBoxColumn.ItemsSource = GoodsCollection;
            dataGrid1.ItemsSource = UsersCollection;
            
            //UnitColumn.ItemsSource = UnitsCollection;
        }
        public class TimeAndSeller {
            public String Time { get; set; }
            public int SellerId { get; set; }
        }

        public class Data
        {
            private int _index = 1;
            public string Vaxt { get; set; }
            public float Miqdar { get; set; }
            public int Id { get; set; }
            public float Qiymet { get; set; }
            public float Mebleq { get; set; }
            public string ProductData { get; set; }
            public int UnitData { get; set; }
            public int Index { get => _index; set => _index = value; }
        }

        private void InitData()
        {
            sqlConnection.Open();
            UpdateGoodsList();
            //new Thread(()=> { while (true) { if (MainWindow.shouldUpdateGoodsList) { this.Dispatcher.Invoke(() => UpdateGoodsList()); MainWindow.shouldUpdateGoodsList = false; } } }).Start();
            //SqlDataAdapter sqlDataUnitsAdapter = new SqlDataAdapter("select * from c_units", sqlConnection);       
            SqlData data = new SqlData(sqlConnection);
            DataTable dataTable1 = data.GetUnitsDataTable();
            DataTable dataTable = data.GetProvidersDataTable();
            //sqlDataProvidersAdapter.Fill(dataTable);
            //sqlDataUnitsAdapter.Fill(dataTable1);
            comboBox.ItemsSource = dataTable.DefaultView;
            comboBox.DisplayMemberPath = dataTable.Columns["ad"].ToString();
            comboBox.SelectedValuePath = dataTable.Columns["id"].ToString();
            UnitColumn.ItemsSource = dataTable1.DefaultView;
            UnitColumn.DisplayMemberPath = dataTable1.Columns["ad"].ToString();
            UnitColumn.SelectedValuePath = dataTable1.Columns["id"].ToString();
            //string type = UnitColumn.GetType().ToString();
            UsersCollection = new ObservableCollection<Data>();
            timeAndSeller = new TimeAndSeller();
            comboBox.DataContext = timeAndSeller;
            Binding comboBinding = new Binding("SellerId") { Mode = BindingMode.TwoWay, NotifyOnSourceUpdated = true };
            comboBox.SetBinding(ComboBox.SelectedValueProperty, comboBinding);
            datePicker.DataContext = timeAndSeller;
            Binding binding = new Binding("Time"){ Mode = BindingMode.TwoWay ,NotifyOnSourceUpdated = true };
            datePicker.SetBinding(DatePicker.TextProperty,binding);
        }
        private void UpdateGoodsList()
        {
            DataTable dataTable = new SqlData(sqlConnection).GetProductsDataTable();
            GoodsBoxColumn.ItemsSource = dataTable.DefaultView;
            GoodsBoxColumn.DisplayMemberPath = dataTable.Columns["ad"].ToString();
            GoodsBoxColumn.SelectedValuePath = dataTable.Columns["id"].ToString();
        }
        //public ObservableCollection<Product> GoodsCollection { get; set; }

        public ObservableCollection<Data> UsersCollection { get; set; }
        //public ObservableCollection<Unit> UnitsCollection { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //dataGrid1.rowde
            int vahidId;
            float miqdar;
            float qiymet;
            string productId;
            float mebleq;
            ObservableCollection<Data> datas = UsersCollection;
            int index;
            //String kontr = ((Product) comboBox.SelectedItem).name.ToString();
            int qaimeN;
            SolidColorBrush lightYellowColor = new SolidColorBrush(Colors.LightYellow);
            if (qaime_N.Text != null && !qaime_N.Text.Equals(""))
                qaimeN = Convert.ToInt32(qaime_N.Text.ToString());
            else
            {
                qaime_N.Background = lightYellowColor;
                MessageBox.Show("Qaimənin nömrəsini daxil edin");
                return;
            }
            String date;
            if (datePicker.SelectedDate != null)
                date = datePicker.Text;
            else
            {
                datePicker.Background = lightYellowColor;
                MessageBox.Show("Qaimənin daxilolma vaxtını daxil edin");
                return;
            }
            bool isAddPressed = false;
            SqlCommand isNumberUniqueCommand = new SqlCommand("exec is_qaime_N_unique @Param1=" + qaimeN, sqlConnection);
            if (int.Parse(qaime_N.Text) != myQaimeNumb)
            {
                if ((Int32)isNumberUniqueCommand.ExecuteScalar() != 1)
                    isQaimeNumbOk = true;
                else
                {
                    MessageBox.Show("Bu nömrəli artıq mövcuddur. Unikal qaimə nömrəsi daxil edin");
                    isQaimeNumbOk = false;
                    return;
                }
            }
            if (isQaimeNumbOk)
            {
                if (!hasSaveButtonBeenPreviouslyPressed)
                    myQaimeNumb = qaimeN;
                if (((Button)sender).Name.Equals("SaveButton"))
                    isAddPressed = false;
                else
                    isAddPressed = true;
                if (hasDateBeenUpdated && hasSaveButtonBeenPreviouslyPressed == true)
                {
                    UpdateTime();
                }
                    if(!isAddPressed)
                    for (int i = 0; i < datas.Count; i++)
                    {
                        foreach (int id in arrayList)
                        {
                            if (datas[i].Id == id && id != 0)
                            {
                                new SqlCommand("update [RestaurantDB].[dbo].[insert_record] set is_active ="+ 0 + ",vaxt = '"+timeAndSeller.Time+"',mal_id = "+
                                    datas[i].ProductData+", malsatan_id ="+ timeAndSeller.SellerId+", vahid_id = "+ datas[i].UnitData +", qiymet ="+
                                    datas[i].Qiymet + ", mebleq =" + datas[i].Mebleq + ", miqdar =" +datas[i].Miqdar+" where id ="+datas[i].Id+ " and qaime_N = "+qaimeN, sqlConnection).ExecuteNonQuery();
                            }
                            else continue;
                        }
                        if (datas[i].Id == 0)
                        {
                            index = datas[i].Index;
                            vahidId = datas[i].UnitData;
                            productId = datas[i].ProductData;
                            miqdar = datas[i].Miqdar;
                            qiymet = datas[i].Qiymet;
                            mebleq = datas[i].Mebleq;
                            String insertCommmandString = "exec insert_new_record @is_active = " + 0 + ",@qiymet =" + qiymet + ",@miqdar =" +
                                miqdar + ",@mebleq =" + mebleq + ",@malsatan_id =" + comboBox.SelectedValue + ",@vahid_id =" + vahidId +
                                ",@tarix = '" + date + "',@qaime_N =" + qaimeN + ",@mal_id =" + productId;
                            SqlCommand insertCommand = new SqlCommand(insertCommmandString, sqlConnection);
                            insertCommand.ExecuteNonQuery();
                            DataTable dataTable = new DataTable("idsTable");
                            String listOfIdsCommand = "select id from [insert_record] where qaime_N = " + qaimeN;
                            new SqlDataAdapter(listOfIdsCommand, sqlConnection).Fill(dataTable);
                            datas[i].Id = int.Parse(dataTable.Rows[i]["id"].ToString());
                        }
                    }
                else
                    {
                    String addCommmandString = "update [RestaurantDB].[dbo].[insert_record] set is_active = 1 where qaime_N ="+qaimeN;
                    SqlCommand addCommand = new SqlCommand(addCommmandString, sqlConnection);
                    addCommand.ExecuteNonQuery();
                    }
                TabControl tabControl = (TabControl)((TabItem)((UserControl1)(dockP.Parent)).Parent).Parent;
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
                if (isAddPressed)
                    MessageBox.Show("Qaimə № " + qaimeN.ToString() + " əlavə edildi");
                else
                {
                    MessageBox.Show("Qaimə № " + qaimeN.ToString() + " yadda saxlanıldı");
                    hasSaveButtonBeenPreviouslyPressed = true;
                }
                MainWindow.shouldUpdateQaimelersList = true;
            }
        }

        private void UpdateTime()
        {
            new SqlCommand("update [RestaurantDB].[dbo].[insert_record] set vaxt = '"+ datePicker.Text+"' where qaime_N = "+qaime_N.Text, sqlConnection).ExecuteNonQuery();
            hasDateBeenUpdated = false;
        }

        private void DataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //e.Row.Header = (e.Row.GetIndex()+1).ToString();            
            if(e.Row.GetIndex()-1>=0 && e.Row.GetIndex()< UsersCollection.Count)
            UsersCollection.ElementAt(e.Row.GetIndex()).Index = e.Row.GetIndex()+1;
        }

        private void DataGrid1_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            dataGrid1.Items.Refresh();
        }

        private void Qaime_N_TextChanged(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(TextBox))
                ((TextBox)sender).Background = new SolidColorBrush(Colors.GhostWhite);
            else if (sender.GetType() == typeof(DatePicker))
                    ((DatePicker)sender).Background = new SolidColorBrush(Colors.GhostWhite);
        }

        private void DataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int index = 0;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                index = e.Row.GetIndex();
                if (e.Column.Header.ToString().Equals("Miqdar"))
                {
                    editingValuesMiqdarArray.Insert(index,float.Parse((e.EditingElement as TextBox).Text));
                }
                else if (e.Column.Header.ToString().Equals("Qiymət"))
                {
                    editingValuesQiymetArray.Insert(index,float.Parse((e.EditingElement as TextBox).Text));
                }
                if((editingValuesQiymetArray.Count>index)&& (editingValuesMiqdarArray.Count > index))
                UsersCollection.ElementAt(index).Mebleq = editingValuesMiqdarArray[index] * editingValuesQiymetArray[index];
                if(!e.Column.Header.ToString().Equals("Məbləğ"))
                (((DataGrid)(sender)).Columns[4].GetCellContent(dataGrid1.Items[index]) as TextBlock).Text = UsersCollection.ElementAt(index).Mebleq.ToString();
                tempData.ProductData = UsersCollection.ElementAt(index).ProductData;
                tempData.Id = UsersCollection.ElementAt(index).Id;
                tempData.Mebleq = UsersCollection.ElementAt(index: index).Mebleq;
                //tempData.MalsatanID = UsersCollection.ElementAt(index).MalsatanID;
                tempData.Miqdar = UsersCollection.ElementAt(index).Miqdar;
                tempData.Qiymet = UsersCollection.ElementAt(index).Qiymet;
                tempData.UnitData = UsersCollection.ElementAt(index).UnitData;
                //tempData.Vaxt = UsersCollection.ElementAt(index).Vaxt;
            }
        }

        private void DataGrid1_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
            if (sender.GetType() == typeof(DataGrid))
            {
                int index = ((DataGrid)sender).SelectedIndex;
                {
                    if (UsersCollection.ElementAt(index) != tempData)
                        if (!arrayList.Contains(tempData.Id))
                            arrayList.Add(tempData.Id);
                }
            }
            else if (sender.GetType() == typeof(DataGrid))
            {
                if (!((DatePicker)sender).Text.Equals(""))
                    return;
                else
                    hasDateBeenUpdated = true;
            }
        }

    }
}