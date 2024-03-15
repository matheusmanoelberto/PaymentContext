
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void SholdReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123",EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void SholdReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("75725320000108", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void SholdReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("75725320000108", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void SholdReturnSuccessWhenCPFIsValid()
        {
            var doc = new Document("11028804008", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}
