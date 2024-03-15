using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace RSCG_Utils;
public class FunctionMemoGenerator 
{
    public void Initialize(IncrementalGeneratorInitializationContext initContext)
    {
        try
        {
            InitializeMe(initContext);
        }
        catch (Exception ex)
        {
            var prov = initContext.CompilationProvider;
            initContext.RegisterSourceOutput(prov, (spc, _) =>
            {

                var dd = new DiagnosticDescriptor("RSCGUTILS_Func_001", "Error", ex.Message + "--" + ex.StackTrace, "Error", DiagnosticSeverity.Error, true);
                var diag = Diagnostic.Create(dd, Location.None);
                spc.ReportDiagnostic(diag);
            }
            );
            //initContext.RegisterSourceOutput(_, (spc, _) => spc.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("RSCG001", "Error", ex.Message, "Error", DiagnosticSeverity.Error, true), Location.None))
        }
    }
    public void InitializeMe(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
    .CreateSyntaxProvider(
        predicate: (s, _) => IsMethodToBeGenerated(s),
        transform: (ctx, _) => GetSemanticTargetForGeneration(ctx))
    .Where(m => m is not null);

        var compilationAndClasses
    = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses,
            (spc, source) => Execute(source.Item1, source.Item2, spc));

    }

    private void Execute(Compilation compilation, ImmutableArray<TypeDeclarationSyntax> classes, SourceProductionContext spc)
    {
        if (classes.IsDefaultOrEmpty)return;
        var distinctClasses = classes.Distinct().ToArray();
        if(((distinctClasses?.Length)??0) == 0) return;
        foreach (var tds in distinctClasses!)
        {
            var nameClass = tds.Identifier.ValueText;
            string nameDefinition = "";
            if(tds is ClassDeclarationSyntax cds)
            {
                nameDefinition= "class";
            }   
            if(tds is RecordDeclarationSyntax rds)
            {
                nameDefinition= "record";
            }
            if(string.IsNullOrWhiteSpace(nameDefinition))
            {
                throw new ArgumentException("only class or record ");
            }
            var ns = tds.Parent as BaseNamespaceDeclarationSyntax;
            var nameNamespace = "";
            while(ns != null)
            {
                nameNamespace = ns.Name.ToFullString();
                ns=ns.Parent as BaseNamespaceDeclarationSyntax;
            }

            var members= tds
                .Members
                .OfType<MethodDeclarationSyntax>()
                .Where(IsMethodToBeGenerated)
                .ToArray();

            foreach (var method in members)
            {
                var nameMethod = method.Identifier.ValueText;
                
                var memo = new SaveMemo(nameDefinition ,nameClass,nameNamespace,method);
                MemoPure.GeneratePartialMemoPure generate = new(memo);
                var text = generate.Render();
                spc.AddSource(memo.fullName(), text);
            }
           

        }

    }

    bool IsMethodToBeGenerated(SyntaxNode node)
    => node is MethodDeclarationSyntax m  &&   m.Identifier.ValueText.Contains("_MemoPure");

    TypeDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        
        var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;
        return methodDeclarationSyntax?.Parent as TypeDeclarationSyntax;

    }

}

