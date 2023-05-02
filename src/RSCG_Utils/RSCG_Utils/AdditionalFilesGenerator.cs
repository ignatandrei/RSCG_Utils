using Microsoft.CodeAnalysis;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace RSCG_Utils
{
    [Generator]
    public class AdditionalFilesGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext initContext)
        {
            IncrementalValuesProvider<AdditionalText> textFiles = initContext
                .AdditionalTextsProvider
                .Where(file => file.Path.Contains(".gen."));

            // read their contents and save their name
            IncrementalValuesProvider<(string name, string content)> namesAndContents = textFiles
                .Select((text, cancellationToken) => (name: Path.GetFileName(text.Path), content: text.GetText(cancellationToken)!.ToString()));
            
            // generate a class that contains their values as const strings
            initContext.RegisterSourceOutput(namesAndContents, (spc, nameAndContent) =>
            {
                
                string nameString=nameAndContent.name
                    .Replace(".","_")
                    .Replace("-", "_")
                    ;
                //v1
                //string[] lines = nameAndContent.content
                //    .Split('\n', '\r')
                //    .Where(it => it.Trim().Length > 0)
                //    .Select(it => it.Replace("\\","\\\\"))
                //    .Select(it=>it.Replace("\"","\\\""))
                //    .Select(it => "\"" + it +"\\r\\n"+ "\"")
                //    .ToArray();
                //string content = string.Join("\r\n+",lines);
                //v2 => .net 7
                string content=nameAndContent.content;       
                //too much, but otherwise start counting quotes
                var quotes = new string('"', 10);
                content = quotes+"\r\n"+ content+"\r\n"+ quotes;
                string hint = $"MyAdditionalFiles.{nameAndContent.name}";
                var str = $@"
    public static partial class MyAdditionalFiles
    {{
        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string                
        public const string {nameString} =  {content};
    }}";
                spc.AddSource(hint,str );
            });
        }
    }
}