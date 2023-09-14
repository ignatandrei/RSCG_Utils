
namespace RSCG_Utils;

[Generator]
public class UtilsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        AdditionalFilesGenerator add = new();
        add.Initialize(context);
        FunctionMemoGenerator funcPure=new();
        funcPure.Initialize(context);
    }
}
