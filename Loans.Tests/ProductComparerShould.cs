using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    public class ProductComparerShould
    {
        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            var products = new List<LoanProduct> { new LoanProduct(1, "a", 1), new LoanProduct(2, "b", 2), new LoanProduct(3, "c", 3) };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // products has 3 items so this test should pass
            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
            var products = new List<LoanProduct> { new LoanProduct(1, "a", 1), new LoanProduct(2, "b", 2), new LoanProduct(3, "c", 3) };

            var sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);

            List<MonthlyRepaymentComparison> comparisons = sut.CompareMonthlyRepayments(new LoanTerm(30));

            // Make sure there are no duplicates
            Assert.That(comparisons, Is.Unique);
        }
    }
}
