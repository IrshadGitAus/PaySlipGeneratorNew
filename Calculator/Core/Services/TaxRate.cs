using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core.Services
{
    public class TaxRate
    {
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal BaseTax { get; set; }
        public decimal Percentage { get; set; }

        public TaxRate(decimal minAmount, decimal maxAmount, decimal baseTax, decimal percentage)
        {
            MinAmount = minAmount;
            MaxAmount = maxAmount;
            BaseTax = baseTax;
            Percentage = percentage;
        }
    }
}
