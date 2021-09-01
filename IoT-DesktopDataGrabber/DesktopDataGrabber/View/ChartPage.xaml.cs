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
    
    /// <summary>
    /// Logika interakcji dla klasy ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {
        public ChartPage()
        {
            InitializeComponent();
        }


        private void test(object sender, RoutedEventArgs e)
        {
            ListView b = e.Source as ListView;
            TextBox t = new TextBox();

            t.Name = "test";
            t.Text = t.Name;
            b.Items.Add(t);

        }
    }
}
