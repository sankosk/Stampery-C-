using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;

namespace API
{
    [TestClass]
    public class UnitTest1
    {
        private Client c;
        

        [TestInitialize()]
        public void Initialize()
        {
            c = new Client("830fa1bf-bee7-4412-c1d3-31dddba2213d");
        }

        [TestMethod]
        public void AuthTest()
        {
            Assert.AreEqual("e8b000333a4bd74", c.TakeN(HashUtils.GetMD5(c.ApiKey), 15));
            Assert.AreEqual("ZThiMDAwMzMzYTRiZDc0OjgzMGZhMWJmLWJlZTctNDQxMi1jMWQzLTMxZGRkYmEyMjEzZA==", c.GetAuth());
        }

    }
}
