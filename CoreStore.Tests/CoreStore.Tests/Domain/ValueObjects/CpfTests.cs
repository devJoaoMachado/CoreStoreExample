using CoreStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreStore.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CpfTests
    {
        [TestMethod]
        public void Should_Return_Notification_When_Document_Is_Not_Valid()
        {
            var document = new Cpf("012345678900");

            Assert.IsFalse(document.Valid);
            Assert.IsTrue(document.Notifications.Count > 0);
        }

        [TestMethod]
        public void Should_Return_Assert_True_When_Document_Is_Valid()
        {
            var document = new Cpf("08056714049");

            Assert.IsTrue(document.Valid);
            Assert.IsTrue(document.Notifications.Count == 0);
        }
    }
}
