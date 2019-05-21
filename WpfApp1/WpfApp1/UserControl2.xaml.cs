using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        DatePicker datePickerFrom, datePickerTo;
        Label providerLabel, warehouseLabel, datePickerLabelFrom, datePickerLabelTo;
        ComboBox providersComboBox, warehousesComboBox;
        Button seachButton;
        private SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
        public UserControl2()
        {
            sqlConnection.Open();
            InitializeComponent();
            InitData(qaime_n: qaime_N.Text);
            //new Thread (UpdateData).Start();
        }

        private void UpdateData()
        {
            while (true)
            {
                if (MainWindow.shouldUpdateQaimelersList)
                {
                    //this.Dispatcher.Invoke(()=> InitData());
                    MainWindow.shouldUpdateQaimelersList = false;
                }
            }
        }
        public void InitData(string qaime_n)
        {
            dataGrid1ListOfImports.ItemsSource = UpdateSource(qaime_n, sqlConnection).DefaultView;
        }

        private void DataGrid1ListOfImports_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {          
            TabControl tabControl = (TabControl)((TabItem)(this.Parent)).Parent;
            if ((((DataGrid)(sender)).SelectedItem) != null)
            {
                TabItem tabItem = new TabItem
                {
                    Content = new UserControl3(Convert.ToInt32(((DataRowView)(((DataGrid)(sender)).SelectedItem)).Row.ItemArray[5].ToString())),
                    Header = new MyStackPanel().NewInstance("Qaimə № " + ((DataRowView)(((DataGrid)(sender)).SelectedItem)).Row.ItemArray[5].ToString())
                };
                tabControl.Items.Add(tabItem);
                tabItem.Focus();
            }
            //MessageBox.Show("qaime nomre "+((DataRowView)(((DataGrid)(sender)).SelectedItem)).Row.ItemArray[3].ToString());
        }

        private void Qaime_N_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitData(((TextBox)sender).Text.ToString());
        }
        private void SelectedDateChanged(string date)
        {
            SqlDataAdapter sqlDataProvidersAdapter = new SqlDataAdapter("SELECT *  FROM [RestaurantDB].[dbo].[qaimeler_list_view] where vaxt like '%" + date + "%'", sqlConnection);
            DataTable dataTable = new DataTable("myData");
            sqlDataProvidersAdapter.Fill(dataTable);
            dataGrid1ListOfImports.ItemsSource = dataTable.DefaultView;
        }

        private void DataGrid1ListOfImports_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure want to delete this record", "Delete Record", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        //int selectedGoodId = UsersCollection.ElementAt(((DataGrid)sender).SelectedIndex).Id;
                        //new SqlCommand("update c_mallar set is_active = 1 where id = " + selectedGoodId, sqlConnection).ExecuteNonQuery();
                        MainWindow.shouldUpdateGoodsList = true;
                        //UpdateGoodsList();
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
        private DataTable UpdateSource(string qaime_n, SqlConnection sqlConnection)
        {
            SqlDataAdapter sqlDataProvidersAdapter = new SqlDataAdapter("SELECT *  FROM [RestaurantDB].[dbo].[qaimeler_list_view] where[qaime_nomre] like '%" + qaime_n + "%'", sqlConnection);
            DataTable dataTable = new DataTable("myData");
            sqlDataProvidersAdapter.Fill(dataTable);
            return dataTable;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Grid.RowDefinitions.Count == 1)
            {
                Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });
                AddUIElements();
            }
            else if (Grid.RowDefinitions.Count == 2)
            {
                int childrenCount = Grid.Children.Count;
                Grid.Children.Remove(datePickerFrom);
                Grid.Children.Remove(datePickerTo);
                Grid.Children.Remove(datePickerLabelFrom);
                Grid.Children.Remove(datePickerLabelTo);
                Grid.Children.Remove(providerLabel);
                Grid.Children.Remove(warehouseLabel);
                Grid.Children.Remove(warehousesComboBox);
                Grid.Children.Remove(providersComboBox);
                Grid.Children.Remove(seachButton);
                Grid.RowDefinitions.RemoveAt(1);
               
            }
            //Grid.Children.Add()
        }
        private void AddUIElements()
        {
            datePickerFrom = new DatePicker() { Height = 23, Width = 190, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(95, 3, 0, 0) };
            datePickerTo = new DatePicker() { Height = 23, Width = 190, VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(95, 0, 0, 3) };
            datePickerLabelFrom = new Label() { Height = 23, Width = 35, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(2), Content ="dan"};
            datePickerLabelTo = new Label() { Height = 23, Width = 35, VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(3), Content = "dək"};
            providerLabel = new Label() { Height = 23, Width = 70, VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(8,2,2,3), Content = "Malsatan"};
            warehouseLabel = new Label() { Height = 23, Width = 42, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(8,3,2,2), Content = "Anbar" };
            warehousesComboBox = new ComboBox() { Height = 22, Width = 150, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 3, 0, 0) };
            providersComboBox = new ComboBox() { Height = 22, Width = 150, VerticalAlignment = VerticalAlignment.Bottom, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 0, 0, 3) };
            seachButton = new Button() { Height = 23, Width = 80, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(-48, 0, 3, 3), Content = "Axtar" };
            Grid.Children.Add(datePickerFrom);
            Grid.Children.Add(seachButton);
            Grid.Children.Add(datePickerTo);
            Grid.Children.Add(providerLabel);
            Grid.Children.Add(warehouseLabel);
            Grid.Children.Add(datePickerLabelFrom);
            Grid.Children.Add(warehousesComboBox);
            Grid.Children.Add(providersComboBox);
            Grid.Children.Add(datePickerLabelTo);
            Grid.SetColumn(seachButton, 4);
            Grid.SetColumnSpan(seachButton, 4);
            Grid.SetRow(seachButton, 1);
            Grid.SetColumnSpan(warehousesComboBox, 2);
            Grid.SetColumn(warehousesComboBox, 1);
            Grid.SetColumnSpan(providersComboBox, 2);
            Grid.SetColumn(providersComboBox, 1);
            Grid.SetColumn(datePickerTo, 2);
            Grid.SetRow(providersComboBox, 1);
            Grid.SetRow(warehousesComboBox, 1);
            Grid.SetRow(providersComboBox, 1);
            Grid.SetRow(datePickerTo, 1);
            Grid.SetColumn(datePickerLabelFrom, 2);
            Grid.SetRow(datePickerLabelFrom, 1);
            Grid.SetColumn(datePickerLabelTo, 2);
            Grid.SetRow(datePickerLabelTo, 1);
            Grid.SetColumn(datePickerFrom, 2);
            Grid.SetRow(datePickerFrom, 1);
            Grid.SetColumn(providerLabel, 0);
            Grid.SetRow(providerLabel, 1);
            Grid.SetColumn(warehouseLabel, 0);
            Grid.SetRow(warehouseLabel, 1);
        }
    }
}
