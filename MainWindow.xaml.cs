using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                }
                await Task.Delay(8000);
            }
        }

        private async void PerformHttpsRequest(double lat, double lon, System.Windows.Controls.Label label)
        {
            try
            {
                string url = "https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=" + apiKey;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();

                    var weatherData = JsonSerializer.Deserialize<WeatherData>(responseData);
                    if (weatherData == null)
                    {
                        return;
                    }

                    string info = ($"Ort: {weatherData.Name}, Temperatur: {weatherData.Main.Temp - 273.15:F2}°C") + "\n" +
                        ($"Wetter: {weatherData.Weather[0].Description}");
                    label.Content = info;
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
            string url = "http://api.openweathermap.org/geo/1.0/direct?limit=5&q=" + City + "&appid=" + apiKey;
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
    }
}