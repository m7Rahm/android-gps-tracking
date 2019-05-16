using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace WpfApp1
{
    public class MyStackPanel : StackPanel
    {
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem currentItem = (TabItem)((StackPanel)(((Button)sender).Parent)).Parent;
            TabControl parentControl = (TabControl)currentItem.Parent;
            if (currentItem != null)
            {
                parentControl.Items.Remove(currentItem);
            }
        }

        public StackPanel NewInstance(string header)
        {
            StackPanel stack = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            Button button = new Button();
            button.Click += Button_Click;
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
                    Button button = (Button)obj;
                    Path path = (Path)button.Content;
                    path.Stroke = new SolidColorBrush(Colors.Black);
                    button.Content = path;
                }
        }
    }
}
