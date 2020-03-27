using NUnit.Framework;
using System;

namespace Loans.Tests
{
    // Defining a .Net attribute, specify how the attribute can be used. Use .Net's 'AttributeUsage' attribute

    // This attribute can be used at the method or class level and doesn't allow multiple uses of this attribute
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    // Inherit from NUnit's 'CategoryAttribute' base class
    class ProductComparisonAttribute : CategoryAttribute
    {
    }
}