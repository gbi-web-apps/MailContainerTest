

using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailContainerTest.Tests
{
    [TestClass]
    public class ServiceLayerTests
    {
        private SimpleInjector.Container injector;
        public ServiceLayerTests()
        {
            injector = new SimpleInjector.Container();
        }

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void T00_1x5eql5()
        {
            var x = 1;
            var y = x * 5;
            Assert.AreEqual(y, 5);
        }

        [TestMethod]
public void T01_MakeMailTransferReturnsFalseIfNoMailContainer()
        {
            var req = new MakeMailTransferRequest();
            req.SourceMailContainerNumber = "no-such-container";
            var mailService = injector.GetInstance<MailTransferService>();
            var result = mailService.MakeMailTransfer(req);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void T01_MakeMailTransferReturnsFalseIfRequestVolumeExceedsContainerCapacity()
        {
            var req = new MakeMailTransferRequest();
            req.NumberOfMailItems = 5;
            var mailService = injector.GetInstance<MailTransferService>();
            var result = mailService.MakeMailTransfer(req);
            Assert.IsFalse(result.Success);
        }


        [TestMethod]
        public void T02_MakeMailTransferReturnsFalseIfMailTypeConflictsWithContainerMailType()
        {
            var req = new MakeMailTransferRequest();
            req.MailType = MailType.LargeLetter;
            var mailService = injector.GetInstance<MailTransferService>();
            var result = mailService.MakeMailTransfer(req);
            Assert.IsFalse(result.Success);
        }


    }
}
