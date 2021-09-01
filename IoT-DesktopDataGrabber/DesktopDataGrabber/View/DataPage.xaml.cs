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
using System.ComponentModel;

namespace DesktopDataGrabber.View
{
    /// <summary>
    /// Logika interakcji dla klasy DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {

    
        public class Data
        {
            public Data(float press, float temp, float hum, float[] joystick , float[] swing, float[] acce, float[] magnetic)
            {
                this.press = press;
                this.temp = temp;
                this.hum = hum;
                this.joystick = joystick;
                this.swing = swing;
                this.acce = acce;
                this.magnetic = magnetic;
            }
            public float press;
            public float temp;
            public float hum;
            public float[] joystick;
            public float[] swing;
            public float[] acce;
            public float[] magnetic;
        }             
        

        public DataPage()
        {
            
            InitializeComponent();
            Preassure.Text = DesktopDataGrabber.ViewModel.MainViewModel.Preassure_val;
            Humidity.Text = DesktopDataGrabber.ViewModel.MainViewModel.Humidity_val;
            Temperature.Text = DesktopDataGrabber.ViewModel.MainViewModel.Temperature_val;

            Pitch.Text = DesktopDataGrabber.ViewModel.MainViewModel.Pitch_val;
            Roll.Text = DesktopDataGrabber.ViewModel.MainViewModel.Roll_val;
            Yaw.Text = DesktopDataGrabber.ViewModel.MainViewModel.Yaw_val;

            SX.Text = DesktopDataGrabber.ViewModel.MainViewModel.SX_val;
            SY.Text = DesktopDataGrabber.ViewModel.MainViewModel.SY_val;
            SS.Text = DesktopDataGrabber.ViewModel.MainViewModel.SS_val;


            MX.Text = DesktopDataGrabber.ViewModel.MainViewModel.Mag_XM_val;
            MY.Text = DesktopDataGrabber.ViewModel.MainViewModel.Mag_YM_val;
            MZ.Text = DesktopDataGrabber.ViewModel.MainViewModel.Mag_ZM_val;

            AX.Text = DesktopDataGrabber.ViewModel.MainViewModel.Acc_Ax_val;
            AY.Text = DesktopDataGrabber.ViewModel.MainViewModel.Acc_Ay_val;
            AZ.Text = DesktopDataGrabber.ViewModel.MainViewModel.Acc_Az_val;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Preassure.Text = DesktopDataGrabber.ViewModel.MainViewModel.Preassure_val;
            Humidity.Text = DesktopDataGrabber.ViewModel.MainViewModel.Humidity_val;
            Temperature.Text = DesktopDataGrabber.ViewModel.MainViewModel.Temperature_val;

            Pitch.Text = DesktopDataGrabber.ViewModel.MainViewModel.Pitch_val;
            Roll.Text = DesktopDataGrabber.ViewModel.MainViewModel.Roll_val;
            Yaw.Text = DesktopDataGrabber.ViewModel.MainViewModel.Yaw_val;

            SX.Text = DesktopDataGrabber.ViewModel.MainViewModel.SX_val;
            SY.Text = DesktopDataGrabber.ViewModel.MainViewModel.SY_val;
            SS.Text = DesktopDataGrabber.ViewModel.MainViewModel.SS_val;

            MX.Text = DesktopDataGrabber.ViewModel.MainViewModel.Mag_XM_val;
            MY.Text = DesktopDataGrabber.ViewModel.MainViewModel.Mag_YM_val;
            MZ.Text = DesktopDataGrabber.ViewModel.MainViewModel.Mag_ZM_val;

            AX.Text = DesktopDataGrabber.ViewModel.MainViewModel.Acc_Ax_val;
            AY.Text = DesktopDataGrabber.ViewModel.MainViewModel.Acc_Ay_val;
            AZ.Text = DesktopDataGrabber.ViewModel.MainViewModel.Acc_Az_val;

        }
    }
}
