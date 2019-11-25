using Calculator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        List<TaxRate> _taxRules = new List<TaxRate>();
        public TaxCalculator()
        {
            _taxRules.Add(new TaxRate(0, 18200, 0, 0));
            _taxRules.Add(new TaxRate(18201, 37000, 0, (decimal)0.19));
            _taxRules.Add(new TaxRate(37001, 80000, 3572, (decimal)0.325));
            _taxRules.Add(new TaxRate(80001, 180000, 17547, (decimal)0.37));
            _taxRules.Add(new TaxRate(180001, decimal.MaxValue, 54547, (decimal)0.45));
        }

        

        public decimal CalculateTax(decimal grossSalary)
        {
            /*
            var taxRuleToBeApplied = _taxRules.Find(t => grossSalary >= t.MinAmount && grossSalary <= t.MaxAmount);
            
            //income tax = (3, 572 + (60, 050 - 37, 000) x 0.325) / 12 = 921.9375(round up) = 922

            var incomeTaxAnnualy = taxRuleToBeApplied.BaseTax + (grossSalary - (taxRuleToBeApplied.MinAmount - 1)) * taxRuleToBeApplied.Percentage;
            var incomeTaxMonthly = Math.Round(incomeTaxAnnualy / 12, MidpointRounding.AwayFromZero);
            return incomeTaxMonthly;
            */

            var taxRuleToBeApplied = _taxRules.Find(t => grossSalary >= t.MinAmount && grossSalary <= t.MaxAmount);

            var incomeTaxAnnualy = taxRuleToBeApplied.BaseTax + (grossSalary - (taxRuleToBeApplied.MinAmount - 1)) * taxRuleToBeApplied.Percentage;
            var incomeTaxMonthly = Math.Round(incomeTaxAnnualy / 12, MidpointRounding.AwayFromZero);
            return incomeTaxMonthly;

        }
    }
}
