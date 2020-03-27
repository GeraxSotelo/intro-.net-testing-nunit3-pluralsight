using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loans.Tests
{
    public class LoanRepaymentCalculatorShould
    {
        [Test]
        // Disadvantage of TestCase attribute is, can't share test data across multiple test methods or test classes
        [TestCase(200_000, 6.5, 30, 1264.14)]
        [TestCase(200_000, 10, 30, 1755.14)]
        [TestCase(500_000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepayment(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        // Disadvantage of TestCase attribute is, can't share test data across multiple test methods or test classes
        [TestCase(200_000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200_000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500_000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment_SimplifiedTestCase(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        // param1: type that contains the test data. param2: as a string, property name that will return the test data
        [TestCaseSource(typeof(MonthlyRepaymentTestData), "TestCases")]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        // param1: type that contains the test data. param2: as a string, property name that will return the test data
        [TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), "TestCases")]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test]
        //Specify a csv file name. Add an additional parameter to TestCaseSource attribute by creating array of objects that map to each of the parameters in the test method.
        // param1: type that contains the test data. param2: as a string, property name that will return the test data. param3: array of objects
        [TestCaseSource(typeof(MonthlyRepaymentCsvData), "GetTestCases", new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_Csv(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }
    }
}
