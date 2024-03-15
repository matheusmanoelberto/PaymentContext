using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void ShoulReturnErrorWhenHadActiveSubscription()
    {
        var name = new Name("Bruce", "Wayne");
        var document = new Document("10320981045", EDocumentType.CPF);
        var email = new Email("matheusmanoel51@gmail.com");
        var student = new Student(name, document, email);

        Assert.Fail();
    }

    [TestMethod]
    public void ShoulReturnSuccessWhenHadNoActiveSubscription()
    {
        Assert.Fail();
    }
}