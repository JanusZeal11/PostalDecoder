using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Threading.Tasks;
using System.IO;

namespace PostalDecoder.Models
{
    public class USPS_PostalObject
    {
        private XDocument usps_post;
        private XDocument usps_get;
        public XDocument USPS_Post { get { return usps_post; } }
        public XDocument USPS_Get { get { return usps_get; } }

        private const String BaseURL = @"http://production.shippingapis.com/ShippingAPI.dll?API=CityStateLookup&XML=";

        public USPS_PostalObject()
        {
            usps_post = new XDocument();
            usps_get = new XDocument();
        }

        public async Task BuildPostalObject(List<string> ZipCodes)
        {
            Format_Request(ZipCodes);
            await CallUSPSAPI();
        }

        public List<String> GetZips()
        {
            List<String> zips = new List<string>();

            foreach (XElement ele in USPS_Post.Root.Descendants("ZipCode"))
            {
                if (ele.Descendants("Error").Count() == 0)
                    zips.Add(ele.Descendants("Zip5").First().Value);
                else
                    zips.Add(ele.Descendants("Description").First().Value);
            }

            return zips;
        }

        public List<String> GetCities()
        {
            List<String> cities = new List<string>();

            foreach (XElement ele in USPS_Post.Root.Descendants("ZipCode"))
            {
                if (ele.Descendants("Error").Count() == 0)
                    cities.Add(ele.Descendants("City").First().Value);
                else
                    cities.Add(ele.Descendants("Description").First().Value);
            }

            return cities;
        }

        public List<String> GetCityStates()
        {
            List<String> states = new List<string>();

            foreach (XElement ele in USPS_Post.Root.Descendants("ZipCode"))
            {
                if (ele.Descendants("Error").Count() == 0)
                    states.Add(ele.Descendants("State").First().Value);
                else
                    states.Add(ele.Descendants("Description").First().Value);
            }

            return states;
        }

        public List<String> GetLocations()
        {
            List<String> locations = new List<String>();

            foreach (XElement ele in USPS_Post.Root.Descendants("ZipCode"))
            {
                if (ele.Descendants("Error").Count() == 0)
                {
                    LocationObject location = new LocationObject(ele.Descendants("City").First().Value.ToString(),
                                                                 ele.Descendants("State").First().Value.ToString(),
                                                                 ele.Descendants("Zip5").First().Value.ToString());

                    locations.Add(location.ToString());
                }
                else
                {
                    locations.Add(ele.Descendants("Description").First().Value);
                }
            }

            return locations;

        }

        public void Format_Request(List<String> ZipCodes)
        {
            XDocument doc = new XDocument(
                new XElement("CityStateLookupRequest",
                    new XAttribute("USERID", "500NA0003814"),
                new XElement("ZipCode",
                    new XAttribute("ID", 0),
                new XElement("Zip5", ZipCodes.First()))));

            if (ZipCodes.Count > 1)
                for (int i = 1; i < ZipCodes.Count; i++)
                    doc.Root.Add(new XElement("ZipCode",
                                    new XAttribute("ID", i),
                                 new XElement("Zip5", ZipCodes[i])));

            usps_post = doc;
        }

        public async Task<XDocument> CallUSPSAPI()
        {
            var result = await GetExternalResponse();

            //File path for Test Case
            string filename = ("../../../PostalDecoder/App_Data/results.xml");
            //File path for normal functionality
            if (HttpContext.Current != null)
                filename = HttpContext.Current.Server.MapPath("~/App_Data/results.xml");

            File.WriteAllText(filename, result);
            usps_post = XDocument.Load(filename);

            return usps_post;
        }

        private async Task<string> GetExternalResponse()
        {
            String url = BaseURL + usps_post.ToString();

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}