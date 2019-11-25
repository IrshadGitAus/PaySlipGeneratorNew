using PaySlipGeneratorNew.Core.Model;

namespace PaySlipGeneratorNew.Infrastructure.Services.Transformers
{
    public interface ITransformerFactory
    {
        ITransformer FetchTransformer(FileExtensionType fileExtensionType);
    }
}