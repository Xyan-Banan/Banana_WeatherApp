using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.OpenWeather
{
    public class Coord
    {
        public double lon;
        public double lat;

        public override string ToString()
        {
            return $"Coord({lon},{lat})";
        }
    }
}
