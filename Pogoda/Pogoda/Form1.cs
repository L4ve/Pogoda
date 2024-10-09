using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Pogoda
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "https://api.open-meteo.com/v1/forecast?latitude=54.36&longitude=18.65&current_weather=true&hourly=temperature_2m,relative_humidity_2m,precipitation";

        public Form1()
        {
            InitializeComponent();
            _ = GetWeatherDataAsync();
        }

        private async Task GetWeatherDataAsync()
        {
            try
            {
                var jsonResponse = await _httpClient.GetStringAsync(ApiUrl);
                var weatherInfo = JObject.Parse(jsonResponse);

                Temperatura.Text = $"Temperatura: {weatherInfo["current_weather"]["temperature"]}°C";
                label2.Text = $"Wilgotność: {weatherInfo["hourly"]["relative_humidity_2m"].First}%";
                label3.Text = $"Opady atmosferyczne: {weatherInfo["hourly"]["precipitation"].First} mm";
                label4.Text = $"Ostatnia aktualizacja: {DateTime.Now}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pobierania danych o pogodzie: {ex.Message}");
            }
        }
    }
}

