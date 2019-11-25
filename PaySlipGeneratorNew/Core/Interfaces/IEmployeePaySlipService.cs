using System.Collections.Generic;
using System.IO;
using PaySlipGeneratorNew.Core.Model;

namespace PaySlipGeneratorNew.Core.Interfaces
{
    public interface IEmployeePaySlipService
    {
        List<EmployeeMonthlyPaySlip> GetEmployeesPaySlip(StreamReader fileStream, FileExtensionType fileExtensionType);
    }
}