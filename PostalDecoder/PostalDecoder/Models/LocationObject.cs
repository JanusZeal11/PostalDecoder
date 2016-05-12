using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PostalDecoder.Models
{
    public class LocationObject
    {
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        
        public LocationObject() { }
        public LocationObject(string city, string state, string zip)
        {
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        public override string ToString()
        {
            return this.City + ", " + this.State + " " + this.Zip;
        }
    }
}