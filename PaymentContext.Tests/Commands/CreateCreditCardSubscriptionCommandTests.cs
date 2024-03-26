using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateCreditCardSubscriptionCommandTests
    {
        [TestMethod]
        public void SholdReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateCreditCardSubscriptionCommand();
            command.FirstName = "";

            command.Validate();
            Assert.AreEqual(false, command.Valid);
        }
    }
}
