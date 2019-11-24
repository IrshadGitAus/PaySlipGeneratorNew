using Calculator.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace Calculator.Tests
{
    [TestClass]
    public class SalaryCalculatorTests
    {
       

        [TestMethod]
        public void Should_Be_Correct_Net_Salary()
        {
            SalaryCalculator calculator = new SalaryCalculator();
            var salary = calculator.CalculateSalary(60050, 9);
            salary.NetIncome.ShouldEqual(4082);
        }
    }
}
