using Calculator.Core.Interfaces;
using Calculator.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace Calculator.Tests
{
    [TestClass]
    public class SalaryCalculatorTests
    {
        private Mock<ITaxCalculator> _mockTaxCalculator;
        
        [TestInitialize]
        public void Initialize()
        {
            _mockTaxCalculator = new Mock<ITaxCalculator>();
        }

        [TestMethod]
        public void Should_Be_Correct_Net_Salary()
        {
            //ITaxCalculator taxCalculator = new TaxCalculator();

            _mockTaxCalculator.Setup(t => t.CalculateTax(It.IsAny<decimal>())).Returns(922);

            SalaryCalculator calculator = new SalaryCalculator(_mockTaxCalculator.Object);

            var salary = calculator.CalculateSalary(60050, 9);


            _mockTaxCalculator.Verify(  (t => t.CalculateTax(It.IsAny<decimal>())), Times.Once);
            //_mockTaxCalculator.Verify((t => t.CalculateTax(It.IsAny<decimal>())), Times.Never);

            salary.NetIncome.ShouldEqual(4082);
        }

        [TestMethod]
        public void Should_Be_Correct_Monthly_Gross_Salary()
        {
            SalaryCalculator salaryCalculator = new SalaryCalculator(_mockTaxCalculator.Object);
            var salary = salaryCalculator.CalculateSalary(60050, 9);

            salary.GrossSalary.ShouldEqual(5004);
        }

    }
}
