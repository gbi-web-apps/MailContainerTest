

using MailContainerTest.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailContainerTest.Tests
{
    [TestClass]
    public class ImplementationLayerTests
    {
        [TestMethod]
        public void T01_LookupContainerReturnsContainer()
        {
            var mailDataStore = new MailContainerDataStore();
            var mailContainer = mailDataStore.GetMailContainer("letter-inservice");
            Assert.IsTrue(mailContainer != null);
        }
    }
}
