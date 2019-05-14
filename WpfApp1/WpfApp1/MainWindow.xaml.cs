using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool shouldUpdateQaimelersList = false;
        public static bool shouldUpdateGoodsList = false;
        int currentPos;
        int newPos;
        ContextMenu contextMenu;
        public MainWindow()
        {
            InitializeComponent();
            currentPos = -1;
            newPos = -1;
            contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem
            {
                Header = "Close"
            };
            menuItem.Click += MenuItem_Click1;
            contextMenu.Items.Add(menuItem);
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            TabItem currentItem = (TabItem)((StackPanel)(((Button) sender).Parent)).Parent;
            TabControl parentControl = (TabControl) currentItem.Parent;
            if (currentItem != null)
            {
                parentControl.Items.Remove(currentItem);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int count = tabControl.Items.Count;
            TabItem currentItem = (TabItem)tabControl.SelectedItem;
            if (currentItem != null)
            {
                currentPos = currentItem.TabIndex;
                if (count > currentPos)
                    tabControl.Items.RemoveAt(currentPos);
                else
                    tabControl.Items.RemoveAt(currentPos - 1);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            newPos++;
            TabItem tabItem = new TabItem
            {
                //DockPanel dockPanel = new DockPanel
                //dockPanel.Children.Add(dataGrid);
                Content = new UserControl1(),
                Header = new MyStackPanel().NewInstance("Sheet "+newPos.ToString())
            };
            //tabItem.MouseRightButtonUp += TabItem_MouseRightButtonDown;
            //ComboBox box = (ComboBox)LogicalTreeHelper.FindLogicalNode(tabItem, "comboBox");
            tabControl.Items.Add(tabItem);
            tabItem.Focus();
        }
        private void DataBase_Connection(ArrayList list)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source = '172.16.3.89'; User = 'sa'; Password = 'P@$$w0rd'; Initial Catalog = 'RestaurantDB'; Integrated Security = false");
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'c_mallar' ORDER BY ORDINAL_POSITION", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            int count = dataTable.Rows.Count;
            for (int i = 0; i < count; i++)
                list.Add(dataTable.Rows[i]["COLUMN_NAME"].ToString());
        }

        private void TabItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TabItem tabIt = sender as TabItem;
            //tabIt.ContextMenu = contextMenu;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //Panel panel = new Panel();
            //dockPan.Children.Add();
            foreach (TabItem tabItem1 in tabControl.Items)
                if (tabItem1.Content.GetType() == typeof(UserControl2))
                {
                    tabItem1.Focus();
                    return;
                }
            TabItem tabItem = new TabItem
            {
                Content = new UserControl2(),
                Header = new MyStackPanel().NewInstance("Jurnal")
            };
            tabControl.Items.Add(tabItem);
            tabItem.Focus();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Name == "anbarQaliqiMenuItem")
            {
                foreach (TabItem tabItem1 in tabControl.Items)
                    if (tabItem1.Content.GetType() == typeof(UserControl6))
                    {
                        tabItem1.Focus();
                        return;
                    }

                TabItem tabItem = new TabItem
                {
                    Content = new UserControl6(),
                    Header = new MyStackPanel().NewInstance("Anbar Qalığı")
                };
                //if(tabItem.Content.GetType()==typeof(UserControl2))
                tabControl.Items.Add(tabItem);
                tabItem.Focus();
            }
            else if (((MenuItem)sender).Name == "mallarMenuItem")
            {
                foreach (TabItem tabItem1 in tabControl.Items)
                    if (tabItem1.Content.GetType() == typeof(UserControl4))
                    {
                        tabItem1.Focus();
                        return;
                    }

                TabItem tabItem = new TabItem
                {
                    Content = new UserControl4(),
                    Header = new MyStackPanel().NewInstance("Mallar")
                };
                //if(tabItem.Content.GetType()==typeof(UserControl2))
                tabControl.Items.Add(tabItem);
                tabItem.Focus();
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            foreach (TabItem tabItem1 in tabControl.Items)
                if (tabItem1.Content.GetType() == typeof(UserControl5))
                {
                    tabItem1.Focus();
                    return;
                }

            TabItem tabItem = new TabItem
            {
                Content = new UserControl5(),
                Header = new MyStackPanel().NewInstance("Malsatanlar")
            };
            //if(tabItem.Content.GetType()==typeof(UserControl2))
            tabControl.Items.Add(tabItem);
            tabItem.Focus();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }
        /*  public class MyStackPanel :StackPanel
 {
     public StackPanel NewInstance(string header)
     {
         StackPanel stack = new StackPanel
         {
             Orientation = Orientation.Horizontal
         };
         Button button = new Button();
         button.Click += new MainWindow().Button_Click;
         Path path = new Path
         {
             Data = Geometry.Parse("M0,0 L7,7 M7, 0 L0,7"),
             SnapsToDevicePixels = true,
             StrokeThickness = 1
         };
         if (button.IsMouseOver)
         {
             path.Stroke = new SolidColorBrush(Colors.White);
             button.Background = new SolidColorBrush(Colors.Azure);
             button.BorderBrush = new SolidColorBrush(Colors.Transparent);
         }
         else
         {
             path.Stroke = new SolidColorBrush(Colors.Black);
         }
         button.Style = (Style)FindResource("CloseButton");
         button.Content = path;
         //button.Height = 10;
         TextBlock textBlock = new TextBlock
         {
             Text = header,
             Margin = new Thickness(3, 0, 3, 0)
         };
         stack.Children.Add(textBlock);
         stack.Children.Add(button);
         stack.MouseEnter += Stack_MouseEnter;
         return stack;
     }

     private void Stack_MouseEnter(object sender, MouseEventArgs e)
     {
         foreach (Object obj in ((StackPanel)sender).Children)
             if (obj.GetType() == typeof(Button))
             {
                 Button button = (Button) obj;
                 Path path = (Path)button.Content;
                 path.Stroke = new SolidColorBrush(Colors.Black);
                 button.Content = path;
             }
     }
 }
*/
    }
}
