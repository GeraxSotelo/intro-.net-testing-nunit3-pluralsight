using Loans.Domain.Applications;
using NUnit.Framework;
using System.Collections.Generic;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanTermShould
    {
        [Test]
        public void ReturnTermInMonths()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.ToMonths(), Is.EqualTo(12), "Months should be 12 times number of years");
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

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);
            // The 'SameAs' method only compares references, not values.
            // Assert that the variables 'a' & 'b' or 'a' & 'c' point to the same object in memory.

            // This one will pass
            Assert.That(a, Is.SameAs(b));
            // This one will fail
            Assert.That(a, Is.SameAs(c));
            // This one will pass
            Assert.That(a, Is.Not.SameAs(c));

            // 'List' of '<type>' is a reference type
            var x = new List<string> { "a", "b" };
            var y = x;
            var z = new List<string> { "a", "b" };

            // This one will pass
            Assert.That(y, Is.SameAs(x));
            // This one will fail
            Assert.That(z, Is.SameAs(x));
            // This one will pass
            Assert.That(z, Is.Not.SameAs(x));
        }

        [Test]
        [Ignore("Testing ignore attribute from NUnit.Framework")]
        public void Double()
        {
            double a = 1.0 / 3.0;

            // This one will fail when dealing with floating point values
            Assert.That(a, Is.EqualTo(0.33));

            // These will pass
            Assert.That(a, Is.EqualTo(0.33).Within(0.004));
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }
    }
}
