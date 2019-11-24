using PaySlipGeneratorNew.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace PaySlipGeneratorNew.Infrastructure.Services.Transformers
{
    public abstract class BaseTransformer : ITransformer
    {
        protected const int HEADER_LINE_INDEX = 1;
        protected const int FILE_COLUMN_COUNT = 5;
        protected const char DELIMITER = ',';
        protected string[] expectedColumnHeaders = new string[] { "First Name", "Last Name", "Annual Salary", "Super Rate (%)", "Payment Start Date" };

        protected virtual void EmptyFileValidation(StreamReader fileStream)
        {
            if (fileStream.Peek() <=0)
            {
                throw new EmptyFileUploadException();
            }
        }
        
        protected virtual void FileHeaderValidation(string[] headerColumns)
        {
            ColumnCountValidation(headerColumns);
            HeaderColumnSequenceValidation(headerColumns);
        }

        protected virtual void ColumnCountValidation(string[] headerColumns)
        {
            if (headerColumns.Length> FILE_COLUMN_COUNT)
            {
                throw new InvalidFileFormatException("there are more than 5 columns!!");
            }

            if (headerColumns.Length < FILE_COLUMN_COUNT)
            {
                throw new InvalidFileFormatException("there are less than 5 columns!!");
            }
        }

        protected virtual void HeaderColumnSequenceValidation(string[] headerColumns)
        {
            if (headerColumns == null)
                throw new InvalidFileFormatException("File has an empty row. Cannot process the file.");
            var errorList = new StringBuilder();
            int i = 0;
            for (i = 0; i < FILE_COLUMN_COUNT; i++)
            {
                if (headerColumns[i].ToUpperInvariant() != expectedColumnHeaders[i].ToUpperInvariant())
                {
                    errorList.Append(headerColumns[i]);
                    errorList.Append(", ");
                }
            }
            if (!string.IsNullOrWhiteSpace(errorList.ToString()))
            {
                errorList.ToString().TrimStart(',', ' ');
                errorList.ToString().TrimEnd(',', ' ');
                throw new InvalidFileFormatException("The columns " + errorList.ToString() + " are incorrect or not in correct sequence.");
            }
        }

        public abstract List<EmployeeMonthlyPaySlip> Transform(StreamReader fileStream);

    }
}
