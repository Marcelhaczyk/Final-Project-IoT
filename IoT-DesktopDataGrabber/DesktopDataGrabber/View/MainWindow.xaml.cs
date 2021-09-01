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

namespace DesktopDataGrabber.View
{
    /** 
     * @brief Interaction logic for MainWindow.xaml 
     */
    public partial class MainWindow : Window
    {
        bool isMenuVisible = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            isMenuVisible = !isMenuVisible;

            if (isMenuVisible)
                this.Menu.Visibility = Visibility.Visible;
            else
                this.Menu.Visibility = Visibility.Collapsed;
        }
        private void ChartBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Source = new Uri("ChartPage.xaml", UriKind.Relative);
        }
        private void LedBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Source = new Uri("LedPage.xaml", UriKind.Relative);
        }
        private void DataBtn_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Source = new Uri("DataPage.xaml", UriKind.Relative);
        }
    }
}
