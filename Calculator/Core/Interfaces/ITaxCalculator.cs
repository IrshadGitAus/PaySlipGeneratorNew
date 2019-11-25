namespace Calculator.Core.Interfaces
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal grossSalary);
    }
}