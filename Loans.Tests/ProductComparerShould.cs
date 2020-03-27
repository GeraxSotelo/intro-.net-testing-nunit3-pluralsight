using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Loans.Tests
{
    public class ProductComparerShould
    {
        private List<LoanProduct> products;
        private ProductComparer sut; //system under test

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // This code gets executed a single time before the first test in the test class executes

            // Simulate long setup init time for this list of products. We assume that this list will not be modified by any tests as this will potentially break other tests (i.e. break test isolation)
            products = new List<LoanProduct> { new LoanProduct(1, "a", 1), new LoanProduct(2, "b", 2), new LoanProduct(3, "c", 3) };
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Run after last test in this test class (fixture) executes. e.g. disposing of shared expensive setup performed in OneTimeSetUp

            // products.Dispose(); e.g. if products implemented IDisposable
        }

        [SetUp]
        public void Setup()
        {
            // This method will get called before each test executes

            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [TearDown]
        public void TearDown()
        {
            // Runs after each test executes
            // if sut implented IDiposable, we can call sut.Dispose();
        }

        [Test]
        // Custom 'ProductComparison' category name is automatically derived from the created class, minus the trailing 'Attribute' word
        [ProductComparison]
        public void ReturnCorrectNumberOfComparisons()
        {
            

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // products has 3 items so this test should pass
            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // Make sure there are no duplicates
            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            // Check that the list of comparisons contains the expected loan product (Will need to know the expected monthly repayment)
            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // assert that the product is in the list, but don't care about the expected monthly repayment value, only that the product is there
            //Assert.That(comparisons, Has.Exactly(1).Property("ProductName").EqualTo("a").And.Property("InterestRate").EqualTo(1).And.Property("MonthlyRepayment").GreaterThan(0));

            // more type-safe way to specify conditions
            Assert.That(comparisons, Has.Exactly(1).Matches<MonthlyRepaymentComparison>(item => item.ProductName == "a" && item.InterestRate == 1 && item.MonthlyRepayment > 0));
        }

        [Test]
        public void NotAllowZeroYears()
        {
            // first param is an action that gets executed and causes and exception to be thrown
            // second param allows to specify that first param action throws an exception
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("Message").EqualTo("Please specify a value greater than 0. (Parameter 'years')"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.EqualTo("Please specify a value greater than 0. (Parameter 'years')"));

            // correct exception and param name but don't care about the message
            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("years"));

            Assert.That(() => new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>().With.Matches<ArgumentOutOfRangeException>(ex => ex.ParamName == "years"));

        }
    }
}
