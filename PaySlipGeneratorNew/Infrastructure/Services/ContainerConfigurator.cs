using Calculator.Core.Interfaces;
using Calculator.Core.Services;
using PaySlipGeneratorNew.Core.Interfaces;
using PaySlipGeneratorNew.Core.Services;
using PaySlipGeneratorNew.Infrastructure.Services.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace PaySlipGeneratorNew.Infrastructure.Services
{
    public class ContainerConfigurator
    {
        public ContainerConfigurator()
        {

        }
    }

    internal static class ExerciseUnityContainer
    {
        public static IUnityContainer Container;

        static ExerciseUnityContainer()
        {
            if (Container==null)
            {
                Container = new UnityContainer();
            }
        }
    }

    public static class ExerciseContainerConfigurator
    {
        public static IUnityContainer ExerciseContainer { get; set; }

        public static void RegisterDependencies()
        {
            ExerciseContainer = ExerciseUnityContainer.Container;

            ExerciseContainer.RegisterType<IEmployeePaySlipService, EmployeePaySlipService>();
            ExerciseContainer.RegisterType<ITransformerFactory, TransformerFactory>();
            ExerciseContainer.RegisterType<IOutputWriter, OutputWriter>();
            ExerciseContainer.RegisterType<ISalaryCalculator, SalaryCalculator>();
            ExerciseContainer.RegisterType<ITaxCalculator, TaxCalculator>();
            //ExerciseContainer.RegisterType<ISuperCalculator, SuperCalculator>();
        }
    }
}
