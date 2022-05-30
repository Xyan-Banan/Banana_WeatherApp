using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.OpenWeather
{
    public class City
    {
        public double id;
        public string name;
        public string state;
        public string country;
        public Coord coord;

        public override string ToString()
        {
            return $"{name} ({country})";
        }

        private sealed class CityComparer : IEqualityComparer<City>
        {
            public bool Equals(City x, City y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.country != y.country) return false;
                //if (x.name == y.name) return false;
                return x.name == y.name;
            }

            public int GetHashCode(City obj)
            {
                return (obj.name != null ? obj.name.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<City> Comparer = new CityComparer();
    }
}
