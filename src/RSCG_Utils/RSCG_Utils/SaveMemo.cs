using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RSCG_Utils;
public class SaveMemo
{ 
    public readonly string className;
    public readonly string nameSpaceName;
    public readonly MethodDeclarationSyntax mds;
    public readonly string nameMethodOriginal;
    public readonly string nameMethodNew;
    public readonly string returnType;
    public readonly string returnTypeNoTask;
    public readonly bool HasArguments;
    public readonly bool IsTask;
    public readonly string[] typeParameters;
    public readonly string[] nameParameters;
    public readonly string[] typeAndNameParameters;
    public SaveMemo(string ClassName, string nameSpaceName, MethodDeclarationSyntax mds)
    {
        className = ClassName;
        this.nameSpaceName = nameSpaceName;
        this.mds = mds;
        nameMethodOriginal = mds.Identifier.ValueText;
        returnType = mds.ReturnType.ToFullString();
        returnTypeNoTask= returnType;
        IsTask = false;
        if (mds.ReturnType is GenericNameSyntax gcs )
        {
            //TODO: maybe later to have more generics
            if (gcs.Identifier.Text=="Task"){
                IsTask = true;
                returnTypeNoTask = gcs.TypeArgumentList.Arguments[0].ToFullString();
            }
        }
        var parameters = mds.ParameterList.Parameters;
        nameMethodNew = nameMethodOriginal.Replace("_MemoPure", "");
        HasArguments = parameters.Count > 0;
        typeParameters = parameters.Select(it=>it.Type!.ToFullString()).ToArray();
        nameParameters= parameters.Select(it=>it.Identifier.ValueText).ToArray();
        typeAndNameParameters= parameters.Select(it=>it.ToFullString()).ToArray();
    }
    public string fullName() => $"{nameSpaceName}_{className}_{nameMethodOriginal}";


}

