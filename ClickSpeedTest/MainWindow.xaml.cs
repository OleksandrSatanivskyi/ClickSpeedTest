using System;
using System.Collections.Generic;
using System.IO;
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

namespace ClickSpeedTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbString.Text = File.ReadAllText("C:\\Users\\Саша\\source\\repos\\ClickSpeedTest\\ClickSpeedTest\\Texts\\Hancock.txt");
        }

        private void Window_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == tbString.Text[0].ToString())
                tbString.Text = tbString.Text.Remove(0,1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (btStart.Content == "Start")
            {
                btStart.Content = "Finish";
                btStart.Background = Brushes.Red;
            }
            else if (btStart.Content == "Finish")
            {
                btStart.Content = "Finish";
                btStart.Background = Brushes.Red;
            }
        }
    }
}
