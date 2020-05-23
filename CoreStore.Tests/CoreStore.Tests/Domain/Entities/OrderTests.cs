using CoreStore.Domain.StoreContext.Entities;
using CoreStore.Domain.StoreContext.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreStore.Tests.Domain.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Customer _validCustomer;

        public OrderTests()
        {
            var name = new Name("João", "Machado");
            var email = new Email("teste123@gmail.com");
            var document = new Cpf("27432158010");
            var phone = "5521998877665";
            _validCustomer = new Customer(name, document, email, phone);
        }


        [TestMethod]
        public void Should_Create_An_Valid_Order_When_Customer_Is_Valid()
        {
            var order = new Order(_validCustomer);
            Assert.IsTrue(order.Valid);
        }



    }
}
