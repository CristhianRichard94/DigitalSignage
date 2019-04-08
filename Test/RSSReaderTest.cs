using System;
using DigitalSignage.BLL.RSSReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace DigitalSignage.Test
{
    [TestClass]
    public class RSSReaderTest
    {

        [TestMethod]
        public void GetFeeds()
        {
            var pRSSReader = new XMLRSSReader();
            var url = "https://www.feedforall.com/sample.xml";
            var result = pRSSReader.Read(url);
            Assert.IsNotNull(result);
        }
    }
}
