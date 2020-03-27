using Loans.Domain.Applications;
using NUnit.Framework.Constraints;

namespace Loans.Tests
{
    // Inherit from NUnit's 'Constraint' class to create a custom constraint
    class MonthlyRepaymentGreaterThanZeroConstraint : Constraint
    {
        public string ExpectedProductName { get; }
        public decimal ExpectedInterestRate { get; }

        public MonthlyRepaymentGreaterThanZeroConstraint(string expectedProductName, decimal expectedInterestRate)
        {
            ExpectedProductName = expectedProductName;
            ExpectedInterestRate = expectedInterestRate;
        }

        // Need to implement the 'ApplyTo' method when inheriting from 'Constraint' class.
        // param: actual value we're testing against
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            // This code decides ultimately whether the assert will pass or fail

            // Test if actual value is of type MonthlyRepaymentComparison
            MonthlyRepaymentComparison comparison = actual as MonthlyRepaymentComparison;

            if (comparison is null) // if actual value is not a MonthlyRepaymentComparison
            {
                // param1: reference to the constraint, 'this'. param2: actual value testing against. param3: constraint status
                return new ConstraintResult(this, actual, ConstraintStatus.Error);
            }

            // if actual value is a MonthlyRepaymentComparison object, we can compare its values with a set of expected values
            if (comparison.InterestRate == ExpectedInterestRate && comparison.ProductName == ExpectedProductName && comparison.MonthlyRepayment > 0)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Success);
            }

            // if InterestRate or ProductName is different OR MonthlyPayment is not greater than 0
            return new ConstraintResult(this, actual, ConstraintStatus.Failure);
        }
    }
}
