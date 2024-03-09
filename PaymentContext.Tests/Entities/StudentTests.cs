using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var student = new Student("Matheus", "Manoel", "12345678","matheusmanoel@gmail.com");
    }
}