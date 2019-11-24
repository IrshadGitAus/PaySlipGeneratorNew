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
    public class DATTransformer : BaseTransformer
    {
        public override List<EmployeeMonthlyPaySlip> Transform(StreamReader fileStream)
        {
            string line;
            var lineIndex = 1;
            EmptyFileValidation(fileStream);
            List<EmployeeMonthlyPaySlip> employeesMonthlyPaySlip = new List<EmployeeMonthlyPaySlip>();

            while ((line=fileStream.ReadLine())!=null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    throw new InvalidFileFormatException();

                line = line.Replace('\t', ' ');
                string[] columns = FormatLineToCSVSeperated(line);

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


        private string[] FormatLineToCSVSeperated(string line)
        {
            StringBuilder formattedLine = new StringBuilder();
            var columns = line.Split(DELIMITER);

            foreach (var column in columns)
            {
                if (!string.IsNullOrWhiteSpace(column))
                {
                    formattedLine.Append(column);
                    formattedLine.Append(DELIMITER);
                }
            }
            columns = formattedLine.ToString().TrimEnd(',').Split(DELIMITER);
            return columns;
        }

    }
}
