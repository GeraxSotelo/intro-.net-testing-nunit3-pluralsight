using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanTermShould
    {
        [Test]
        public void ReturnTermInMonths()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12));
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = 1;
            var b = 1;
            // Ints in C# are value types, 'Is.EqualTo()' will perform a value comparison.
            // The values of 1 and 1 are the same. This assert should pass.
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueEqualityRef()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);
            // In C#, classes are reference types, not value types
            // Two reference types, even if they have the same values, won't be considered equal by the 'Is.EqualTo()' method.
            // Because in 'ValueObject.cs', the Equals method has been overridden with custom logic, it makes use of the 'GetAtomicValues()' method
            // In 'LoanTerm.cs' the 'GetAtomicValues()' method has been overridden and it returns the 'Years'
            // So in this assert, if both 'LoanTerm()' have the same values for 'Years', they will be considered equal.
            Assert.That(a, Is.EqualTo(b));
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}
