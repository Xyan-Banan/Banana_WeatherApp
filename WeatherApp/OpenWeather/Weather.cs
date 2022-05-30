using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;

namespace WeatherApp.OpenWeather
{
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        [JsonProperty("icon")]
        public string iconName;
        public Bitmap Icon
        {
            get { return new Bitmap($"icons/{iconName}.png"); }
        }
    }
}
