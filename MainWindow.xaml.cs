using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;

namespace WetterApp
{
    public partial class MainWindow : Window
    {

        private string apiKey = "8940de4f448e4403be7edbd658e37125";

        public MainWindow()
        {
            InitializeComponent();
            requestLoop();
        }

        private async void requestLoop()
        {
            while (true)
            {
                if (MyComboBox.SelectedItem is Location selectedItem)
                {
                    PerformHttpsRequest(selectedItem.lat, selectedItem.lon, MyLabel);
                    FetchForecastData(selectedItem.lat, selectedItem.lon);
                }
                await Task.Delay(300000);
            }
        }



        private async void PerformHttpsRequest(double lat, double lon, System.Windows.Controls.Label label)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&lang=de";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();

                    var weatherData = JsonSerializer.Deserialize<WeatherData>(responseData);
               
                    if (weatherData == null) return;

                    LocationText.Text = $"{weatherData.Name}, {weatherData.Sys.Country}";
                    DateText.Text = DateTime.Now.ToString("dddd dd MMMM");
                    CurrentTempText.Text = $"{(weatherData.Main.Temp - 273.15):F0}°C";
                    WeatherDescText.Text = weatherData.Weather[0].Description;
                    HighTempText.Text = $"{(weatherData.Main.Temp_Max - 273.15):F0}°C";
                    LowTempText.Text = $"{(weatherData.Main.Temp_Min - 273.15):F0}°C";
                    WindText.Text = $"{(weatherData.Wind.Speed * 2.237):F0}mph";

                    var sunriseTime = DateTimeOffset.FromUnixTimeSeconds(weatherData.Sys.Sunrise).LocalDateTime;
                    var sunsetTime = DateTimeOffset.FromUnixTimeSeconds(weatherData.Sys.Sunset).LocalDateTime;

                    SunriseText.Text = sunriseTime.ToString("HH:mm");
                    SunsetText.Text = sunsetTime.ToString("HH:mm");

                    RainText.Text = weatherData.Rain?.OneHour.ToString("F0") + "%" ?? "0%";

                    var IconUrl = $"https://openweathermap.org/img/wn/{weatherData.Weather[0].Icon}.png";

                    WeatherIcon.Source = new BitmapImage(new Uri(IconUrl));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der Anfrage: {ex.Message}");
            }
        }

        public void TextUpdated(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }
            GetCity(MyTextBox.Text);
        }

        public async void GetCity(string City)
        {
            if (City == null || City.Length == 0)
            {
                MyComboBox.IsEnabled = false;
                MyComboBox.ItemsSource = null;
                MyComboBox.SelectedIndex = 0;
                MyComboBox.DisplayMemberPath = "displayName";
                return;
            }
            string url = $"http://api.openweathermap.org/geo/1.0/direct?limit=5&q={City}&appid={apiKey}&lang=de";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<List<Location>>(responseData);
                    if (result == null || result.Count == 0)
                    {
                        return;
                    }
                    MyComboBox.ItemsSource = result;
                    MyComboBox.SelectedIndex = 0;
                    MyComboBox.DisplayMemberPath = "displayName";
                    MyComboBox.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler bei der Anfrage: {ex.Message}");
            }
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MyComboBox.SelectedItem == null)
            {
                MyLabel.Content = string.Empty;
            }
            if (MyComboBox.SelectedItem is Location selectedItem)
            {
                PerformHttpsRequest(selectedItem.lat, selectedItem.lon, MyLabel);
            }
        }

        private async void FetchForecastData(double lat, double lon)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={apiKey}&lang=de&units=metric";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();
                    var forecastData = JsonSerializer.Deserialize<ForecastData>(responseData);

                    if (forecastData == null || forecastData.List == null) return;

                    var groupedData = forecastData.List
                        .Where(item => DateTime.Parse(item.DtTxt).Date > DateTime.Today)
                        .GroupBy(item => DateTime.Parse(item.DtTxt).Date)
                        .Select(group => new
                        {
                            Date = group.Key.ToString("dddd"),
                            MaxTemp = $"{(group.Max(item => item.Main.Temp_Max)):F0}",
                            MinTemp = $"{(group.Min(item => item.Main.Temp_Min)):F0}",
                            Description = group.First().Weather[0].Description,
                            IconUrl = $"https://openweathermap.org/img/wn/{group.First().Weather[0].Icon}.png"
                        })
                        .ToList();

                    ForecastList.ItemsSource = groupedData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Abrufen der Wettervorhersage: {ex.Message}");
            }
        }
    }
}