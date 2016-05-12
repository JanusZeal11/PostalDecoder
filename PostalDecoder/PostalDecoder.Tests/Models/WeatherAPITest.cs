using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PostalDecoder.Models;

namespace PostalDecoder.Tests.Models
{
    [TestClass]
    public class WeatherAPITest
    {
        [TestMethod]
        public void FormatRequest()
        {
            var testRequest = GetRequest();

            var model = new WeatherAPI();
            var return1 = model.Format_Request(testRequest);
            Assert.AreEqual(return1, testReturn1.ToString());
        }

        private String GetRequest()
        {
            var testRequest = "98513,us";
            return testRequest;
        }
    }
}
