using System.Collections.Generic;
using PaySlipGeneratorNew.Core.Model;

namespace PaySlipGeneratorNew.Core.Interfaces
{
    public interface IOutputWriter
    {
        void Write(List<EmployeeMonthlyPaySlip> employeesMonthlyPaySlip);
    }
}