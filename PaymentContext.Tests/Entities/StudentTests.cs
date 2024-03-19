using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;
    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("10320981045", EDocumentType.CPF);
        _email = new Email("matheusmanoel51@gmail.com");
        _address = new Address("Rua 1", "1234", "Bairro legal", "Gotham", "DF", "BR", "72640111");
        _student = new Student(_name, _document, _email);
        _subscription =  new Subscription(null);
        
    }

    [TestMethod]
    public void ShoulReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShoulReturnErrorWhenSubscriptionNoPayment()
    {        
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Invalid);
    }

    [TestMethod]
    public void ShoulReturnSuccessWhenAddSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Invalid);
    }
}