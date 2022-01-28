using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Drawing;

namespace WeatherAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getForecast("Osijek");
        }
        
        void getForecast(string cityName)
        {
            string url = String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=5&APPID=542ffd081e67f4512b705f89d2a611b2",cityName);
            using(WebClient web = new WebClient())
            {
                var json = web.DownloadString(url);
                var Object = JsonConvert.DeserializeObject<Forecast>(json);
                Forecast forecast = Object;
                lbl_city.Content = string.Format("{0}, {1}", forecast.city.name, forecast.city.country);
                //day 0 (today)
                lbl_temp_0.Content = string.Format("{0}\u00B0C", forecast.list[0].temp.day);
                lbl_desc_0.Content = string.Format("{0}", forecast.list[0].weather[0].description);
                lbl_ws_0.Content = string.Format("wind speed: {0} km/h", forecast.list[0].speed);
                lbl_hum_0.Content = string.Format("humidity: {0} %", forecast.list[0].humidity);
                img_day0.Source = getIcon(forecast.list[0].weather[0].icon);
                //day 1
                lbl_date_1.Content = string.Format("{0} {1}",getDate(forecast.list[1].dt).ToString("ddd"), getDate(forecast.list[1].dt).Day);
                lbl_desc_1.Content = string.Format("{0}", forecast.list[1].weather[0].description);
                lbl_temp_1.Content = string.Format("{0}\u00B0C", forecast.list[1].temp.day);
                lbl_ws_1.Content = string.Format("wind speed: {0} km/h", forecast.list[1].speed);
                lbl_hum_1.Content = string.Format("humidity: {0} %", forecast.list[1].humidity);
                img_day_1.Source = getIcon(forecast.list[1].weather[0].icon);
                //day 2
                lbl_date_2.Content = string.Format("{0} {1}", getDate(forecast.list[2].dt).ToString("ddd"), getDate(forecast.list[2].dt).Day);
                lbl_desc_2.Content = string.Format("{0}", forecast.list[2].weather[0].description);
                lbl_temp_2.Content = string.Format("{0}\u00B0C", forecast.list[1].temp.day);
                lbl_ws_2.Content = string.Format("wind speed: {0} km/h", forecast.list[2].speed);
                lbl_hum_2.Content = string.Format("humidity: {0} %", forecast.list[2].humidity);
                img_day_2.Source = getIcon(forecast.list[2].weather[0].icon);
                //day 3
                lbl_date_3.Content = string.Format("{0} {1}", getDate(forecast.list[3].dt).ToString("ddd"), getDate(forecast.list[3].dt).Day);
                lbl_desc_3.Content = string.Format("{0}", forecast.list[3].weather[0].description);
                lbl_temp_3.Content = string.Format("{0}\u00B0C", forecast.list[1].temp.day);
                lbl_ws_3.Content = string.Format("wind speed: {0} km/h", forecast.list[3].speed);
                lbl_hum_3.Content = string.Format("humidity: {0} %", forecast.list[3].humidity);
                img_day_3.Source = getIcon(forecast.list[3].weather[0].icon);
                //day 4
                lbl_date_4.Content = string.Format("{0} {1}", getDate(forecast.list[4].dt).ToString("ddd"), getDate(forecast.list[4].dt).Day);
                lbl_desc_4.Content = string.Format("{0}", forecast.list[4].weather[0].description);
                lbl_temp_4.Content = string.Format("{0}\u00B0C", forecast.list[4].temp.day);
                lbl_ws_4.Content = string.Format("wind speed: {0} km/h", forecast.list[4].speed);
                lbl_hum_4.Content = string.Format("humidity: {0} %", forecast.list[4].humidity);
                img_day_4.Source = getIcon(forecast.list[4].weather[0].icon);
            }
        }

        DateTime getDate(double millisecound)
        {
            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();
            return day;
        }

        BitmapImage getIcon(string iconID)
        {
            string urll = string.Format("http://openweathermap.org/img/w/{0}.png", iconID); // weather icon resource 

            BitmapImage weatherImg = new BitmapImage();
            weatherImg.BeginInit();
            weatherImg.UriSource = new Uri(urll);
            weatherImg.EndInit();

            return weatherImg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtb_cityName.Text == "")
                getForecast("Osijek");
            else
                getForecast(txtb_cityName.Text);
        }
    }
}
