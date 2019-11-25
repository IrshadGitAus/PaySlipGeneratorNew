using Calculator.Core.Interfaces;
using Calculator.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Services
{
    public class SalaryCalculator : ISalaryCalculator
    {
        private ITaxCalculator _taxCalculator;
        public SalaryCalculator(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        public SalaryDto CalculateSalary(decimal annualSalary, decimal superPercantage)
        {

            //gross income = 60,050 / 12 = 5,004.16666667(round down) = 5,004
            //income tax = (3, 572 + (60, 050 - 37, 000) x 0.325) / 12 = 921.9375(round up) = 922
            //net income = 5,004 - 922 = 4,082
            //super = 5,004 x 9 % = 450.36(round down) = 450

            SalaryDto salary = new SalaryDto();

            //gross income
            var grossMonthlySalary = Math.Round(annualSalary / 12, MidpointRounding.AwayFromZero);
            salary.GrossSalary = grossMonthlySalary;

            //income tax
            //TaxCalculator tc = new TaxCalculator();

            salary.IncomeTax = _taxCalculator.CalculateTax(annualSalary);

            //salary.IncomeTax= _taxCalculator.CalculateTax(annualSalary);
            //salary.IncomeTax = _taxCalculator.CalculateTax(annualSalary);

            //salary.IncomeTax = 922;



            //Net income
            var netIncome = grossMonthlySalary - salary.IncomeTax;
            salary.NetIncome = Math.Round(netIncome, MidpointRounding.AwayFromZero);

            //Super
            var superAmount = netIncome * (superPercantage / 100);
            salary.Super = Math.Round(superAmount, MidpointRounding.AwayFromZero);

            return salary;

        }



    }
}
