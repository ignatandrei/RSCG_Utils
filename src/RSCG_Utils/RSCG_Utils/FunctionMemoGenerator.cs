using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace RSCG_Utils;
public class FunctionMemoGenerator 
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
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

    private void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes, SourceProductionContext spc)
    {
        if (classes.IsDefaultOrEmpty)return;
        var distinctClasses = classes.Distinct().ToArray();
        if(((distinctClasses?.Length)??0) == 0) return;
        foreach (var cds in distinctClasses!)
        {
            var nameClass = cds.Identifier.ValueText;
            var ns = cds.Parent as BaseNamespaceDeclarationSyntax;
            var nameNamespace = "";
            while(ns != null)
            {
                nameNamespace = ns.Name.ToFullString();
                ns=ns.Parent as BaseNamespaceDeclarationSyntax;
            }

            var members= cds
                .Members
                .OfType<MethodDeclarationSyntax>()
                .Where(IsMethodToBeGenerated)
                .ToArray();

            foreach (var method in members)
            {
                var nameMethod = method.Identifier.ValueText;
                
                var memo = new SaveMemo(nameClass,nameNamespace,method);
                MemoPure.GeneratePartialMemoPure generate = new(memo);
                var text = generate.Render();
                spc.AddSource(memo.fullName(), text);
            }
           

        }

    }

    bool IsMethodToBeGenerated(SyntaxNode node)
    => node is MethodDeclarationSyntax m  &&   m.Identifier.ValueText.Contains("_MemoPure");

    ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;
        return methodDeclarationSyntax?.Parent as ClassDeclarationSyntax;
    }

}

