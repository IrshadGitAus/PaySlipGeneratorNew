using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaySlipGeneratorNew.Core.Model;
using Utilities.Exceptions;
using Utilities.Extensions;

namespace PaySlipGeneratorNew.Infrastructure.Services.Transformers
{
    public class CSVTransformer : BaseTransformer
    {
        public override List<EmployeeMonthlyPaySlip> Transform(StreamReader fileStream)
        {
            string line;
            var lineIndex = 1;
            var employeesMonthlyPaySlip = new List<EmployeeMonthlyPaySlip>();

            EmptyFileValidation(fileStream);

            while ((line = fileStream.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    throw new InvalidFileFormatException();

                var columns = line.Split(DELIMITER);

                if (lineIndex == HEADER_LINE_INDEX)
                {
                    FileHeaderValidation(columns);
                    lineIndex++;
                    continue;
                }

                ColumnCountValidation(columns);

                var employeeMonthlyPaySlip = new EmployeeMonthlyPaySlip
                {
                    FirstName = columns[0].Trim(),
                    LastName = columns[1].Trim(),
                    AnnualSalary = columns[2].ToNumber("Annual Salary", lineIndex),
                    SuperRate = columns[3].ToNumber("Super Rate (%)", lineIndex),
                    PaymentStartDate = columns[4].Trim(),
                };
                employeesMonthlyPaySlip.Add(employeeMonthlyPaySlip);
                lineIndex++;
            }
            return employeesMonthlyPaySlip;
        }
    }
}
