using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostalDecoder.Models
{
    public class WeatherObject
    {
        public String LocationName { get; set; }
        public String CurrentConditions { get; set; }
        public String Temperature { get; set; }
        public String WindSpeed { get; set; }

        public WeatherObject() { }
        public WeatherObject(string location, string conditions, string temp, string wind)
        {
            this.LocationName = location;
            this.CurrentConditions = conditions;
            this.Temperature = temp;
            this.WindSpeed = wind;
        }

        public override string ToString()
        {
            return this.LocationName + ": " + this.CurrentConditions + ", " + this.Temperature + ", WS:" + WindSpeed;
        }
    }
}