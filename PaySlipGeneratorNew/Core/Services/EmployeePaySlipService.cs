using Calculator.Core.Interfaces;
using Calculator.Core.Model;
using Calculator.Core.Services;
using PaySlipGeneratorNew.Core.Interfaces;
using PaySlipGeneratorNew.Core.Model;
using PaySlipGeneratorNew.Infrastructure.Services.Transformers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySlipGeneratorNew.Core.Services
{

    public class EmployeePaySlipService : IEmployeePaySlipService
    {
        private readonly ITransformerFactory _transformerfactory;
        private readonly ISalaryCalculator _salaryCalculator;
        private ITransformer _transformer;

        public EmployeePaySlipService(ITransformerFactory transformerfactory, ISalaryCalculator salaryCalculator)
        {
            _transformerfactory = transformerfactory;
            _salaryCalculator = salaryCalculator;
        }

        public List<EmployeeMonthlyPaySlip> GetEmployeesPaySlip(StreamReader fileStream, FileExtensionType fileExtensionType)
        {

            try
            {
                //write a transform that will read the filestream and give a list by mapping each row in the stream to EmployeeMonthlyPaySlip object
                //TransformerFactory _transformerFactory = new TransformerFactory();
                _transformer = _transformerfactory.FetchTransformer(fileExtensionType);

                //now we got a list of all the employee payslip info.
                var employeesMonthlyPaySlip = _transformer.Transform(fileStream);

                //SalaryCalculator _salaryCalculator = new SalaryCalculator();

                //Next step is to calculate the salary part and append the salary part to the above employee payslip object
                foreach (var employee in employeesMonthlyPaySlip)
                {
                    var salary = _salaryCalculator.CalculateSalary(employee.AnnualSalary, employee.SuperRate);
                    employee.Salary = new Salary()
                    {
                        GrossSalary = salary.GrossSalary,
                        IncomeTax = salary.IncomeTax,
                        NetIncome = salary.NetIncome,
                        Super = salary.Super
                    };

                }

                return employeesMonthlyPaySlip;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
