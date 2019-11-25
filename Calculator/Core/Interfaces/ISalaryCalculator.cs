using Calculator.Core.Model;

namespace Calculator.Core.Interfaces
{
    public interface ISalaryCalculator
    {
        SalaryDto CalculateSalary(decimal annualSalary, decimal superPercantage);
    }
}