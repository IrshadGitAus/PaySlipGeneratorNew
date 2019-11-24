using PaySlipGeneratorNew.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySlipGeneratorNew.Infrastructure.Services.Transformers
{
    public interface ITransformer
    {
        List<EmployeeMonthlyPaySlip> Transform(StreamReader fileStream);
    }
}
