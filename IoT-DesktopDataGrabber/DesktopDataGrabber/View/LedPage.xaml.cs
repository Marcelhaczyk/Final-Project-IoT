using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DesktopDataGrabber.View
{
    using Model;
    /// <summary>
    /// Logika interakcji dla klasy LedPage.xaml
    /// </summary>
    public partial class LedPage : Page
    {

        public class Data
        {
            
            public Data(int x, int y, int r, int g, int b)
            {
                this.x = x;
                this.y = y;
                this.r = r;
                this.g = g;
                this.b = b; 
            }
            public int x = 0;
            public int y = 0;
            public int r = 0;
            public int g = 0;
            public int b = 0;
        }

        string json = "[{'x':0,'y':0,'r':0,'g':0,'b':0},{'x':1,'y':0,'r':0,'g':0,'b':0},{'x':2,'y':0,'r':105,'g':105,'b':105},{'x':3,'y':0,'r':105,'g':105,'b':105},{'x':4,'y':0,'r':105,'g':105,'b':105},{'x':5,'y':0,'r':105,'g':105,'b':105},{'x':6,'y':0,'r':0,'g':0,'b':0},{'x':7,'y':0,'r':0,'g':0,'b':0},{'x':0,'y':1,'r':0,'g':0,'b':0},{'x':1,'y':1,'r':105,'g':105,'b':105},{'x':2,'y':1,'r':105,'g':105,'b':105},{'x':3,'y':1,'r':105,'g':105,'b':105},{'x':4,'y':1,'r':105,'g':105,'b':105},{'x':5,'y':1,'r':105,'g':105,'b':105},{'x':6,'y':1,'r':105,'g':105,'b':105},{'x':7,'y':1,'r':0,'g':0,'b':0},{'x':0,'y':2,'r':105,'g':105,'b':105},{'x':1,'y':2,'r':105,'g':105,'b':105},{'x':2,'y':2,'r':165,'g':42,'b':42},{'x':3,'y':2,'r':105,'g':105,'b':105},{'x':4,'y':2,'r':105,'g':105,'b':105},{'x':5,'y':2,'r':165,'g':42,'b':42},{'x':6,'y':2,'r':105,'g':105,'b':105},{'x':7,'y':2,'r':105,'g':105,'b':105},{'x':0,'y':3,'r':105,'g':105,'b':105},{'x':1,'y':3,'r':105,'g':105,'b':105},{'x':2,'y':3,'r':105,'g':105,'b':105},{'x':3,'y':3,'r':105,'g':105,'b':105},{'x':4,'y':3,'r':105,'g':105,'b':105},{'x':5,'y':3,'r':105,'g':105,'b':105},{'x':6,'y':3,'r':105,'g':105,'b':105},{'x':7,'y':3,'r':105,'g':105,'b':105},{'x':0,'y':4,'r':0,'g':0,'b':0},{'x':1,'y':4,'r':105,'g':105,'b':105},{'x':2,'y':4,'r':105,'g':105,'b':105},{'x':3,'y':4,'r':169,'g':169,'b':169},{'x':4,'y':4,'r':169,'g':169,'b':169},{'x':5,'y':4,'r':105,'g':105,'b':105},{'x':6,'y':4,'r':105,'g':105,'b':105},{'x':7,'y':4,'r':0,'g':0,'b':0},{'x':0,'y':5,'r':0,'g':0,'b':0},{'x':1,'y':5,'r':0,'g':0,'b':0},{'x':2,'y':5,'r':105,'g':105,'b':105},{'x':3,'y':5,'r':105,'g':105,'b':105},{'x':4,'y':5,'r':105,'g':105,'b':105},{'x':5,'y':5,'r':105,'g':105,'b':105},{'x':6,'y':5,'r':0,'g':0,'b':0},{'x':7,'y':5,'r':0,'g':0,'b':0},{'x':0,'y':6,'r':0,'g':0,'b':0},{'x':1,'y':6,'r':105,'g':105,'b':105},{'x':2,'y':6,'r':105,'g':105,'b':105},{'x':3,'y':6,'r':105,'g':105,'b':105},{'x':4,'y':6,'r':105,'g':105,'b':105},{'x':5,'y':6,'r':105,'g':105,'b':105},{'x':6,'y':6,'r':105,'g':105,'b':105},{'x':7,'y':6,'r':0,'g':0,'b':0},{'x':0,'y':7,'r':105,'g':105,'b':105},{'x':1,'y':7,'r':105,'g':105,'b':105},{'x':2,'y':7,'r':0,'g':0,'b':0},{'x':3,'y':7,'r':105,'g':105,'b':105},{'x':4,'y':7,'r':105,'g':105,'b':105},{'x':5,'y':7,'r':0,'g':0,'b':0},{'x':6,'y':7,'r':105,'g':105,'b':105},{'x':7,'y':7,'r':105,'g':105,'b':105}]";
        SolidColorBrush ColorVal = new SolidColorBrush();
        List<Data> rgb_data = new List<Data>();
        string rgb_json="";
        public LedPage()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Data temp = new Data(i, j, 0, 0, 0);
                    rgb_data.Add(temp);
                    
                }

            }
            InitializeComponent();
        }

        async private void dataSender()
        {
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string s = "b";
                    s += i;
                    s += j;
                    Button x = FindName(s) as Button;
                    Brush z = x.Background;
                    if (z == null) 
                    {
                        Data temp = new Data(j, i, 0, 0, 0);
                        rgb_data[k]= temp;
                    }
                    else
                    {

                        Color color = (Color)ColorConverter.ConvertFromString(z.ToString());
                        Data temp = new Data(j, i, color.R, color.G, color.B);
                        rgb_data[k] = temp;
                    }
                    k++;
                }

            }
            rgb_json = JsonConvert.SerializeObject(rgb_data.ToArray());
            //System.IO.File.WriteAllText("data.json", rgb_json);

            

            using (HttpClient client = new HttpClient())  {
               var requestDataCollection = new List <KeyValuePair <string , string >>(); 
                requestDataCollection.Add(new KeyValuePair <string , string >("data", rgb_json)); 
                var requestData = new FormUrlEncodedContent(requestDataCollection); 
                var result = await client.PostAsync("http://192.168.56.101/reader.php", requestData); 
                var responseText = await result.Content.ReadAsStringAsync();
                Console.WriteLine(responseText);
            }



        }
            public void CreeperBtnOnClick(object sender, RoutedEventArgs e)
            {
            JArray crp_json = JArray.Parse(json);
            // JObject arr = JObject.Parse(json);
             
            int k = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color color = new Color();

                    color = Color.FromRgb((byte)crp_json[k]["r"], (byte)crp_json[k]["g"], (byte)crp_json[k]["b"]);

                    SolidColorBrush c = new SolidColorBrush(color);
                    string s = "b";
                    s += crp_json[k]["y"];
                    s += crp_json[k]["x"];
                    Button x = FindName(s) as Button;
                    x.Background = c;
                    Data temp = new Data((int)crp_json[k]["y"], (int)crp_json[k]["x"], (int)crp_json[k]["r"], (int)crp_json[k]["g"], (int)crp_json[k]["b"]);
                    rgb_data[k] = temp;
                    k++;
                }

            }
            dataSender();
        }
        public void clearAllBtn(object sender, RoutedEventArgs e)
        {
            int k = 0;
            for (int i=0; i<8; i++)
            {
                for(int j =0; j<8; j++)
                {
                    string s = "b";
                    s += i;
                    s += j;
                    Button x = FindName(s) as Button;
                    x.Background = null;
                    Data temp = new Data(i, j, 0, 0, 0);
                    rgb_data[k] = temp;
                    k++;
                }
                
            }
            
            
            dataSender();
        }
        public void onBtnClick(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            b.Background = ColorVal;

            dataSender();
        }
        private void cp_SelectedColorChanged_1(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (cp.SelectedColor.HasValue)
            {
                ColorVal = new SolidColorBrush(cp.SelectedColor.Value);
                Color C = cp.SelectedColor.Value;
                var Red = C.R;
                var Green = C.G;
                var Blue = C.B;
                long colorVal = Convert.ToInt64(Blue * (Math.Pow(256, 0)) + Green * (Math.Pow(256, 1)) + Red * (Math.Pow(256, 2)));
            }

        }

    }
}

