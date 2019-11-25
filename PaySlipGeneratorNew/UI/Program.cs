using PaySlipGeneratorNew.Core.Interfaces;
using PaySlipGeneratorNew.Infrastructure.Services;
using PaySlipGeneratorNew.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PaySlipGeneratorNew
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            ExerciseContainerConfigurator.RegisterDependencies();
            var employeePaySlipService = ExerciseContainerConfigurator.ExerciseContainer.Resolve<IEmployeePaySlipService>();
            var outputFileWriter = ExerciseContainerConfigurator.ExerciseContainer.Resolve<IOutputWriter>();
            Application.Run(new MYOBExercise(employeePaySlipService, outputFileWriter));
        }
    }
}
