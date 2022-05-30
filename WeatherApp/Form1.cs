using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        string city = "498817";
        string appId = "04a0a624f0486a756afcec98cc60509b";

        public Form1()
        {
            InitializeComponent();
        }

        async void getWeather()
        {
            WebRequest request = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?id={city}&lang=ru&appid={appId}");
            request.Method = "GET";
            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = await request.GetResponseAsync();

            string answer = string.Empty;

            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                answer = await reader.ReadToEndAsync();
            }

            response.Close();

            OpenWeather.OpenWeather ow = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(answer);
            richTextBox1.Text = JsonConvert.SerializeObject(ow, Formatting.Indented).Replace("  ", "\t");
            OpenWeather.Weather weather = ow.weather[0];

            Icon.Image = weather.Icon;
            this.weather.Text = weather.main;
            description.Text = weather.description;

            averageTemp.Text = "Средняя температура (°С): " + ow.main.temp.ToString("0.#");
            windSpeed.Text = "Скорость ветра (м/с): " + ow.wind.speed.ToString();
            windDir.Text = "Направление °: " + ow.wind.deg.ToString();

            humidity.Text = "Влажнсть (%): " + ow.main.humidity.ToString();
            pressure.Text = "Давление (мм): " + ((int)ow.main.pressure).ToString();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            getWeather();

            string citiesJson = await File.OpenText("city.list.json").ReadToEndAsync();
            List<OpenWeather.City> cities = JsonConvert.DeserializeObject<List<OpenWeather.City>>(citiesJson)
                .Distinct(OpenWeather.City.Comparer)
                .ToList();
            cities.Sort((c1, c2) => c1.name.CompareTo(c2.name));
            cities.RemoveAll(c1 => c1.name == "-");
            this.cities.Items.AddRange(cities.ToArray());
        }

        private void cities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedCity = ((OpenWeather.City)cities.SelectedItem);
            city = ((int)selectedCity.id).ToString();
            Console.WriteLine($"{selectedCity.country} {selectedCity.name} {selectedCity.id} {selectedCity.coord}");

            getWeather();
        }
    }
}
