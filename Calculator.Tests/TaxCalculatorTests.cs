using Calculator.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace Calculator.Tests
{
    [TestClass]
    public class TaxCalculatorTests
    {
        [TestMethod]
        public void Should_Be_Zero_Tax_When_Income_Is_Not_In_Tax_Bracket()
        {
            TaxCalculator calculator = new TaxCalculator();
            var tax = calculator.CalculateTax(18200);
            tax.ShouldEqual(0);
        }

        [TestMethod]
        public void Should_Be_Caclulated_According_To_19_Percent_Rule_Tax_When_Income_Is_18001_Lower_EdgeCase()
        {
            TaxCalculator calculator = new TaxCalculator();
            var tax = calculator.CalculateTax(18201);
            tax.ShouldEqual(0); //shouldn't this be 19c
        }

        [TestMethod]
        public void Should_Be_Caclulated_According_To_19_Percent_Rule_Tax_When_Income_Is_18001_Higher_EdgeCase()
        {
            TaxCalculator calculator = new TaxCalculator();
            var tax = calculator.CalculateTax(37000);
            tax.ShouldEqual(298);
        }
    }
}
