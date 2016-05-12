using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.Linq;
using PostalDecoder.Models;
using System.Threading.Tasks;

namespace PostalDecoder.Tests.Models
{
    [TestClass]
    public class USPS_PostalObjectTest
    {
        [TestMethod]
        public void Format_Request1()
        {
            var testRequest1 = GetRequestZip();
            var testReturn1 = GetFormatedRequestZip();

            var model = new USPS_PostalObject();
            model.Format_Request(testRequest1);
            var return1 = model.USPS_Post;
            Assert.AreEqual(return1.ToString(), testReturn1.ToString());
        }
        [TestMethod]
        public void Format_RequestN()
        {
            var testRequestN = GetRequestZips();
            var testReturnN = GetFormatedRequestZips();

            var model = new USPS_PostalObject();
            model.Format_Request(testRequestN);
            var return1 = model.USPS_Post;
            Assert.AreEqual(return1.ToString(), testReturnN.ToString());
        }

        private List<String> GetRequestZip()
        {
            var testRequest = new List<String>();
            testRequest.Add("98513");
            return testRequest;
        }
        private List<String> GetRequestZip_Error()
        {
            var testRequest = new List<String>();
            testRequest.Add("55555");
            return testRequest;
        }
        private List<String> GetRequestZips()
        {
            var testRequest = new List<String>();
            testRequest.Add("98513");
            testRequest.Add("97080");
            testRequest.Add("90210");
            testRequest.Add("53012");
            return testRequest;

        }
        private List<String> GetRequestZips_Error()
        {
            var testRequest = new List<String>();
            testRequest.Add("98513");
            testRequest.Add("97080");
            testRequest.Add("55555");
            testRequest.Add("53012");
            return testRequest;

        }
        private XDocument GetFormatedRequestZip()
        {
            XDocument doc = new XDocument(
               new XElement("CityStateLookupRequest",
                   new XAttribute("USERID", "500NA0003814"),
               new XElement("ZipCode",
                   new XAttribute("ID", 0),
               new XElement("Zip5", "98513"))));

            return doc;
        }
        private XDocument GetFormatedRequestZips()
        {
            XDocument doc = new XDocument(
               new XElement("CityStateLookupRequest",
                   new XAttribute("USERID", "500NA0003814"),
               new XElement("ZipCode",
                   new XAttribute("ID", 0),
               new XElement("Zip5", "98513")),
               new XElement("ZipCode",
                   new XAttribute("ID", 1),
               new XElement("Zip5", "97080")),
               new XElement("ZipCode",
                   new XAttribute("ID", 2),
               new XElement("Zip5", "90210")),
               new XElement("ZipCode",
                   new XAttribute("ID", 3),
               new XElement("Zip5", "53012"))));

            return doc;
        }

        [TestMethod]
        public async Task CallUSPSAPI1()
        {
            var testResponse1 = GetFormattedResponseZip();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip());
            var response1 = await model.CallUSPSAPI();
            Assert.AreEqual(response1.ToString(), testResponse1.ToString());
        }
        [TestMethod]
        public async Task CallUSPSAPI1_Error()
        {
            var testResponse1 = GetFormattedResponseZip_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip_Error());
            var response1 = await model.CallUSPSAPI();
            Assert.AreEqual(response1.ToString(), testResponse1.ToString());
        }
        [TestMethod]
        public async Task CallUSPSAPIN()
        {
            var testResponseN = GetFormattedResponseZips();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips());
            var responseN = await model.CallUSPSAPI();
            Assert.AreEqual(responseN.ToString(), testResponseN.ToString());
        }
        [TestMethod]
        public async Task CallUSPSAPIN_Error()
        {
            var testResponseN = GetFormattedResponseZips_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips_Error());
            var responseN = await model.CallUSPSAPI();
            Assert.AreEqual(responseN.ToString(), testResponseN.ToString());
        }

        private XDocument GetFormattedResponseZip()
        {
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "UTF-8", null),
              new XElement("CityStateLookupResponse",
              new XElement("ZipCode",
                  new XAttribute("ID", 0),
              new XElement("Zip5", "98513"),
              new XElement("City", "OLYMPIA"),
              new XElement("State", "WA"))));

            return doc;
        }
        private XDocument GetFormattedResponseZip_Error()
        {
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "UTF-8", null),
              new XElement("CityStateLookupResponse",
              new XElement("ZipCode",
                  new XAttribute("ID", 0),
              new XElement("Error",
              new XElement("Number", -2147219399),
              new XElement("Source", "WebtoolsAMS;CityStateLookup"),
              new XElement("Description", "Invalid Zip Code."),
              new XElement("HelpFile"),
              new XElement("HelpContext")))));

            return doc;
        }
        private XDocument GetFormattedResponseZips()
        {
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "UTF-8", null),
              new XElement("CityStateLookupResponse",
              new XElement("ZipCode",
                  new XAttribute("ID", 0),
              new XElement("Zip5", "98513"),
              new XElement("City", "OLYMPIA"),
              new XElement("State", "WA")),
              new XElement("ZipCode",
                  new XAttribute("ID", 1),
              new XElement("Zip5", "97080"),
              new XElement("City", "GRESHAM"),
              new XElement("State", "OR")),
              new XElement("ZipCode",
                  new XAttribute("ID", 2),
              new XElement("Zip5", "90210"),
              new XElement("City", "BEVERLY HILLS"),
              new XElement("State", "CA")),
              new XElement("ZipCode",
                  new XAttribute("ID", 3),
              new XElement("Zip5", "53012"),
              new XElement("City", "CEDARBURG"),
              new XElement("State", "WI"))));

            return doc;
        }
        private XDocument GetFormattedResponseZips_Error()
        {
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "UTF-8", null),
              new XElement("CityStateLookupResponse",
              new XElement("ZipCode",
                  new XAttribute("ID", 0),
              new XElement("Zip5", "98513"),
              new XElement("City", "OLYMPIA"),
              new XElement("State", "WA")),
              new XElement("ZipCode",
                  new XAttribute("ID", 1),
              new XElement("Zip5", "97080"),
              new XElement("City", "GRESHAM"),
              new XElement("State", "OR")),
              new XElement("ZipCode",
                  new XAttribute("ID", 2),
              new XElement("Error",
              new XElement("Number", -2147219399),
              new XElement("Source", "WebtoolsAMS;CityStateLookup"),
              new XElement("Description", "Invalid Zip Code."),
              new XElement("HelpFile"),
              new XElement("HelpContext"))),
              new XElement("ZipCode",
                  new XAttribute("ID", 3),
              new XElement("Zip5", "53012"),
              new XElement("City", "CEDARBURG"),
              new XElement("State", "WI"))));

            return doc;
        }

        [TestMethod]
        public async Task GetZips1()
        {
            var testReturn1 = GetZip();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip());
            await model.CallUSPSAPI();
            var return1 = model.GetZips();
            Assert.AreEqual(return1[0], testReturn1[0]);
        }
        [TestMethod]
        public async Task GetZips1_Error()
        {
            var testReturn1 = GetZip_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip_Error());
            await model.CallUSPSAPI();
            var return1 = model.GetZips();
            Assert.AreEqual(return1[0], testReturn1[0]);
        }
        [TestMethod]
        public async Task GetZipsN()
        {
            var testReturnN = GetZips();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips());
            await model.CallUSPSAPI();
            var returnN = model.GetZips();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i], testReturnN[i]);
            }
        }
        [TestMethod]
        public async Task GetZipsN_Error()
        {
            var testReturnN = GetZips_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips_Error());
            await model.CallUSPSAPI();
            var returnN = model.GetZips();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i], testReturnN[i]);
            }
        }

        public List<String> GetZip()
        {
            var list = new List<String>();
            list.Add("98513");
            return list;
        }
        public List<String> GetZip_Error()
        {
            var list = new List<String>();
            list.Add("Invalid Zip Code.");
            return list;
        }
        public List<String> GetZips()
        {
            var list = new List<String>();
            list.Add("98513");
            list.Add("97080");
            list.Add("90210");
            list.Add("53012");
            return list;
        }
        public List<String> GetZips_Error()
        {
            var list = new List<String>();
            list.Add("98513");
            list.Add("97080");
            list.Add("Invalid Zip Code.");
            list.Add("53012");
            return list;
        }

        [TestMethod]
        public async Task GetCities1()
        {
            var testReturn1 = GetCity();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip());
            await model.CallUSPSAPI();
            var return1 = model.GetCities();
            Assert.AreEqual(return1[0], testReturn1[0]);
        }
        [TestMethod]
        public async Task GetCities1_Error()
        {
            var testReturn1 = GetCity_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip_Error());
            await model.CallUSPSAPI();
            var return1 = model.GetCities();
            Assert.AreEqual(return1[0], testReturn1[0]);
        }
        [TestMethod]
        public async Task GetCitiesN()
        {
            var testReturnN = GetCities();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips());
            await model.CallUSPSAPI();
            var returnN = model.GetCities();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i], testReturnN[i]);
            }
        }
        [TestMethod]
        public async Task GetCitiesN_Error()
        {
            var testReturnN = GetCities_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips_Error());
            await model.CallUSPSAPI();
            var returnN = model.GetCities();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i], testReturnN[i]);
            }
        }

        public List<String> GetCity()
        {
            var list = new List<String>();
            list.Add("OLYMPIA");
            return list;
        }
        public List<String> GetCity_Error()
        {
            var list = new List<String>();
            list.Add("Invalid Zip Code.");
            return list;
        }
        public List<String> GetCities()
        {
            var list = new List<String>();
            list.Add("OLYMPIA");
            list.Add("GRESHAM");
            list.Add("BEVERLY HILLS");
            list.Add("CEDARBURG");
            return list;
        }
        public List<String> GetCities_Error()
        {
            var list = new List<String>();
            list.Add("OLYMPIA");
            list.Add("GRESHAM");
            list.Add("Invalid Zip Code.");
            list.Add("CEDARBURG");
            return list;
        }

        [TestMethod]
        public async Task GetCityStates1()
        {
            var testReturn1 = GetState();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip());
            await model.CallUSPSAPI();
            var return1 = model.GetCityStates();
            Assert.AreEqual(return1[0], testReturn1[0]);
        }
        [TestMethod]
        public async Task GetCityStates1_Error()
        {
            var testReturn1 = GetState_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip_Error());
            await model.CallUSPSAPI();
            var return1 = model.GetCityStates();
            Assert.AreEqual(return1[0], testReturn1[0]);
        }
        [TestMethod]
        public async Task GetCityStatesN()
        {
            var testReturnN = GetStates();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips());
            await model.CallUSPSAPI();
            var returnN = model.GetCityStates();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i], testReturnN[i]);
            }
        }
        [TestMethod]
        public async Task GetCityStatesN_Error()
        {
            var testReturnN = GetStates_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips_Error());
            await model.CallUSPSAPI();
            var returnN = model.GetCityStates();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i], testReturnN[i]);
            }
        }

        public List<String> GetState()
        {
            var list = new List<String>();
            list.Add("WA");
            return list;
        }
        public List<String> GetState_Error()
        {
            var list = new List<String>();
            list.Add("Invalid Zip Code.");
            return list;
        }
        public List<String> GetStates()
        {
            var list = new List<String>();
            list.Add("WA");
            list.Add("OR");
            list.Add("CA");
            list.Add("WI");
            return list;
        }
        public List<String> GetStates_Error()
        {
            var list = new List<String>();
            list.Add("WA");
            list.Add("OR");
            list.Add("Invalid Zip Code.");
            list.Add("WI");
            return list;
        }

        [TestMethod]
        public async Task GetLocation1()
        {
            var testReturn1 = GetLocation();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip());
            await model.CallUSPSAPI();
            var return1 = model.GetLocations();
            Assert.AreEqual(return1[0].ToString(), testReturn1[0].ToString());
        }
        [TestMethod]
        public async Task GetLocation1_Error()
        {
            var testReturn1 = GetLocation_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZip_Error());
            await model.CallUSPSAPI();
            var return1 = model.GetLocations();
            Assert.AreEqual(return1[0].ToString(), testReturn1[0].ToString());
        }
        [TestMethod]
        public async Task GetLocationN()
        {
            var testReturnN = GetLocations();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips());
            await model.CallUSPSAPI();
            var returnN = model.GetLocations();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i].ToString(), testReturnN[i].ToString());
            }
        }
        [TestMethod]
        public async Task GetLocationN_Error()
        {
            var testReturnN = GetLocations_Error();

            var model = new USPS_PostalObject();
            model.Format_Request(GetRequestZips_Error());
            await model.CallUSPSAPI();
            var returnN = model.GetLocations();
            Assert.AreEqual(returnN.Count, testReturnN.Count);
            for (int i = 0; i < returnN.Count; i++)
            {
                Assert.AreEqual(returnN[i].ToString(), testReturnN[i].ToString());
            }
        }

        private List<string> GetLocation()
        {
            var list = new List<string>();
            list.Add("OLYMPIA, WA 98513");
            return list;
        }
        private List<string> GetLocation_Error()
        {
            var list = new List<string>();
            list.Add("Invalid Zip Code.");
            return list;
        }
        private List<string> GetLocations()
        {
            var list = new List<string>();
            list.Add("OLYMPIA, WA 98513");
            list.Add("GRESHAM, OR 97080");
            list.Add("BEVERLY HILLS, CA 90210");
            list.Add("CEDARBURG, WI 53012");
            return list;
        }
        private List<string> GetLocations_Error()
        {
            var list = new List<string>();
            list.Add("OLYMPIA, WA 98513");
            list.Add("GRESHAM, OR 97080");
            list.Add("Invalid Zip Code.");
            list.Add("CEDARBURG, WI 53012");
            return list;
        }
    }
}
