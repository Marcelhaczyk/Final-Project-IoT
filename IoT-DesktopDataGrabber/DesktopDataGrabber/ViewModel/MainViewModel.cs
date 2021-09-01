#define CLIENT
#define GET
#define DYNAMIC


using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Http;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;




namespace DesktopDataGrabber.ViewModel
{

    
    using Model;
    using System.Windows;

    /** 
      * @brief View model for MainWindow.xaml 
      */
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        //[0]
        public static string Preassure_val="0";
        //[1]
        public static string Humidity_val = "0";
        //[2]
        public static string Temperature_val = "0";
        //[3]
        public static string Pitch_val = "0";
        public static string Roll_val = "0";
        public static string Yaw_val = "0";
        //[4]
        public static string SX_val = "0";
        public static string SY_val = "0";
        public static string SS_val = "0";

        //[5]
        public static string Mag_XM_val = "0";
        public static string Mag_YM_val = "0";
        public static string Mag_ZM_val = "0";

        //[6]

        public static string Acc_Ax_val = "0";
        public static string Acc_Ay_val = "0";
        public static string Acc_Az_val = "0";













        private string ipAddress;
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }
        private int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }

        public PlotModel DataPlotModelPressure { get; set; }
        public PlotModel DataPlotModelTemperature { get; set; }
        public PlotModel DataPlotModelHummidity { get; set; }
        public PlotModel DataPlotModelSwing { get; set; }

        public ButtonCommand StartButton { get; set; }
        public ButtonCommand StopButton { get; set; }
        public ButtonCommand UpdateConfigButton { get; set; }
        public ButtonCommand DefaultConfigButton { get; set; }

        //public static string Preassure_val { get; set; }
        #endregion

        #region Fields
        private int timeStamp = 0;
        private ConfigParams config = new ConfigParams();
        private Timer RequestTimer;
        private IoTServer Server;
        #endregion

        public MainViewModel()
        {
            
            DataPlotModelPressure = new PlotModel { Title = "Pressure Data" };
            DataPlotModelTemperature = new PlotModel { Title = "Temperature Data" };
            DataPlotModelHummidity = new PlotModel { Title = "Humidity Data" };
            DataPlotModelSwing = new PlotModel { Title = "Swing data" };

            DataPlotModelPressure.Axes.Add(new LinearAxis()
            {
                
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            DataPlotModelPressure.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 1500,
                IsZoomEnabled = false,
                Key = "Vertical",
                Unit = "mbar",
                Title = "Pressure"
            });
           

            DataPlotModelPressure.Series.Add(new LineSeries() { Title = "Pressure data series", Color = OxyColor.Parse("#FFFF0000") });


            DataPlotModelTemperature.Axes.Add(new LinearAxis()
            {
                
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            DataPlotModelTemperature.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 200,
                IsZoomEnabled = false,
                Key = "Vertical",
                Unit = "C",
                Title = "Temperature"
            });
           

            DataPlotModelTemperature.Series.Add(new LineSeries() { Title = "Temperature data series", Color = OxyColor.Parse("#FFFF0000") });
            
            DataPlotModelHummidity.Axes.Add(new LinearAxis()
            {
                
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            DataPlotModelHummidity.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                IsZoomEnabled = false,
                Key = "Vertical",
                Unit = "%",
                Title = "Humidity"
            });
           

            DataPlotModelHummidity.Series.Add(new LineSeries() { Title = "Humidity data series", Color = OxyColor.Parse("#FFFF0000") });
            DataPlotModelSwing.Axes.Add(new LinearAxis()
            {

                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            DataPlotModelSwing.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 360,
                IsZoomEnabled = false,
                Key = "Vertical",
                Unit = "Degrees",
                Title = "Swing"
            });

            DataPlotModelSwing.Series.Add(new LineSeries() { Title = "Pitch data series", Color = OxyColor.Parse("#FFFF0000") });
            DataPlotModelSwing.Series.Add(new LineSeries() { Title = "Roll data series", Color = OxyColor.Parse("#AAAA0000") });
            DataPlotModelSwing.Series.Add(new LineSeries() { Title = "Yaw data series", Color = OxyColor.Parse("#FFFFF000") });
            StartButton = new ButtonCommand(StartTimer);
            StopButton = new ButtonCommand(StopTimer);
            UpdateConfigButton = new ButtonCommand(UpdateConfig);
            DefaultConfigButton = new ButtonCommand(DefaultConfig);

            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;

            Server = new IoTServer(IpAddress);
        }

        /**
          * @brief Time series plot update procedure.
          * @param t X axis data: Time stamp [ms].
          * @param d Y axis data: Real-time measurement [-].
          */
        private void UpdatePlotPressure(double t, double d)
        {
            LineSeries lineSeriesPressure = DataPlotModelPressure.Series[0] as LineSeries;
           

            lineSeriesPressure.Points.Add(new DataPoint(t, d));

            if (lineSeriesPressure.Points.Count > config.MaxSampleNumber)
                lineSeriesPressure.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                DataPlotModelPressure.Axes[0].Minimum = (t - config.XAxisMax);
                DataPlotModelPressure.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            DataPlotModelPressure.InvalidatePlot(true);
        }
        private void UpdatePlotTemperature(double t, double d)
        {
            LineSeries lineSeriesTemperature= DataPlotModelTemperature.Series[0] as LineSeries;
           

            lineSeriesTemperature.Points.Add(new DataPoint(t, d));

            if (lineSeriesTemperature.Points.Count > config.MaxSampleNumber)
                lineSeriesTemperature.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                DataPlotModelTemperature.Axes[0].Minimum = (t - config.XAxisMax);
                DataPlotModelTemperature.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            DataPlotModelTemperature.InvalidatePlot(true);
        }
        private void UpdatePlotHummidity(double t, double d)
        {
            LineSeries lineSeriesHummidity = DataPlotModelHummidity.Series[0] as LineSeries;
           

            lineSeriesHummidity.Points.Add(new DataPoint(t, d));

            if (lineSeriesHummidity.Points.Count > config.MaxSampleNumber)
                lineSeriesHummidity.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                DataPlotModelHummidity.Axes[0].Minimum = (t - config.XAxisMax);
                DataPlotModelHummidity.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            DataPlotModelHummidity.InvalidatePlot(true);
        }
        private void UpdatePlotSwing(double t, double roll, double pitch, double yaw)
        {
            LineSeries lineSeriesRoll = DataPlotModelSwing.Series[0] as LineSeries;
            LineSeries lineSeriesPitch = DataPlotModelSwing.Series[1] as LineSeries;
            LineSeries lineSeriesYaw = DataPlotModelSwing.Series[2] as LineSeries;


            lineSeriesPitch.Points.Add(new DataPoint(t, pitch));
            lineSeriesRoll.Points.Add(new DataPoint(t, roll));
            lineSeriesYaw.Points.Add(new DataPoint(t, yaw));

            if (lineSeriesPitch.Points.Count > config.MaxSampleNumber)
                lineSeriesPitch.Points.RemoveAt(0);
            if (lineSeriesRoll.Points.Count > config.MaxSampleNumber)
                lineSeriesRoll.Points.RemoveAt(0);
            if (lineSeriesYaw.Points.Count > config.MaxSampleNumber)
                lineSeriesYaw.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                DataPlotModelSwing.Axes[0].Minimum = (t - config.XAxisMax);
                DataPlotModelSwing.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            DataPlotModelSwing.InvalidatePlot(true);
        }
        /**
          * @brief Asynchronous chart update procedure with
          *        data obtained from IoT server responses.
          * @param ip IoT server IP address.
          */
        private async void UpdatePlotWithServerResponse()
        {
#if CLIENT
#if GET
            string responseText = await Server.GETwithClient();
#else
            string responseText = await Server.POSTwithClient();
#endif
#else
#if GET
            string responseText = await Server.GETwithRequest();
#else
            string responseText = await Server.POSTwithRequest();
#endif
#endif
            try
            {
#if DYNAMIC
                dynamic resposneJson = JArray.Parse(responseText);
                UpdatePlotPressure(timeStamp / 1000.0, (double)resposneJson[0].Pressure);

                Preassure_val = resposneJson[0].Pressure;
                Humidity_val = resposneJson[2].Humidity;
                Temperature_val = resposneJson[1].Temperature;

                Pitch_val = resposneJson[3].Swing[0].Pitch;
                Roll_val = resposneJson[3].Swing[1].Roll;
                Yaw_val = resposneJson[3].Swing[2].Yaw;

                SX_val = resposneJson[4].Joystick[0].SX;
                SY_val = resposneJson[4].Joystick[1].SY;
                SS_val = resposneJson[4].Joystick[2].SS;

                Mag_XM_val = resposneJson[5].Magnetic[0].XM;
                Mag_YM_val= resposneJson[5].Magnetic[1].YM;
                Mag_ZM_val = resposneJson[5].Magnetic[2].ZM;

                Acc_Ax_val = resposneJson[6].Acceleration[0].AX;
                Acc_Ay_val = resposneJson[6].Acceleration[1].AY;
                Acc_Az_val = resposneJson[6].Acceleration[2].AZ;





                //Preassure_string = resposneJson[0].Pressure;

                Console.WriteLine("Preassure measurements:" + Preassure_val + "hPa");

                // Preassure_val is a string value
                //Console.WriteLine("Preassure_val type is " + Preassure_val.GetType());

                UpdatePlotTemperature(timeStamp / 1000.0, (double)resposneJson[1].Temperature);
                //Console.WriteLine("Temperature measurements:" + (double)resposneJson[1].Temperature + "Celsius Degrees");
                UpdatePlotHummidity(timeStamp / 1000.0, (double)resposneJson[2].Humidity);
                Console.WriteLine("Humidity measurements:" + Humidity_val + "%");

                UpdatePlotSwing(timeStamp / 1000.0, (double)resposneJson[3].Swing[0].Pitch, (double)resposneJson[3].Swing[1].Roll, (double)resposneJson[3].Swing[2].Yaw);

#else
                ServerData resposneJson = JsonConvert.DeserializeObject<ServerData>(responseText);
                UpdatePlot(timeStamp / 1000.0, (double)resposneJson.Pressure);
#endif
            }
            catch (Exception e)
            {
                Debug.WriteLine("JSON DATA ERROR");
                Debug.WriteLine(responseText);
                Debug.WriteLine(e);
            }

            timeStamp += config.SampleTime;
        }

        /**
          * @brief Synchronous procedure for request queries to the IoT server.
          * @param sender Source of the event: RequestTimer.
          * @param e An System.Timers.ElapsedEventArgs object that contains the event data.
          */
        private void RequestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePlotWithServerResponse();
        }

        #region ButtonCommands

        /**
         * @brief RequestTimer start procedure.
         */
        private void StartTimer()
        {
            if (RequestTimer == null)
            {
                RequestTimer = new Timer(config.SampleTime);
                RequestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElapsed);
                RequestTimer.Enabled = true;

                DataPlotModelPressure.ResetAllAxes();
                DataPlotModelTemperature.ResetAllAxes();
                DataPlotModelHummidity.ResetAllAxes();
            }
          //  Console.WriteLine("dziala1");
        }

        /**
         * @brief RequestTimer stop procedure.
         */
        private void StopTimer()
        {
            if (RequestTimer != null)
            {
                RequestTimer.Enabled = false;
                RequestTimer = null;
            }
          //  Console.WriteLine("dziala2");
        }

        /**
         * @brief Configuration parameters update
         */
        private void UpdateConfig()
        {
            bool restartTimer = (RequestTimer != null);

            if (restartTimer)
                StopTimer();

            config = new ConfigParams(ipAddress, sampleTime);
            Server = new IoTServer(IpAddress);

            if (restartTimer)
                StartTimer();
           // Console.WriteLine("dziala3");
        }

        /**
          * @brief Configuration parameters defualt values
          */
        private void DefaultConfig()
        {
            bool restartTimer = (RequestTimer != null);

            if (restartTimer)
                StopTimer();

            config = new ConfigParams();
            IpAddress = config.IpAddress;
            SampleTime = config.SampleTime.ToString();
            Server = new IoTServer(IpAddress);

            if (restartTimer)
                StartTimer();
            Console.WriteLine("dziala4");
        }
        
        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * @brief Simple function to trigger event handler
         * @params propertyName Name of ViewModel property as string
         */
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        /*
        static PreassureUpdate()
        {
            Preassure_val = "Model Dimension";
        }
        */
    }
}
