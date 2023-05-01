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

                string[] lines = nameAndContent.content
                    .Split('\n', '\r')
                    .Where(it => it.Trim().Length > 0)
                    .Select(it => it.Replace("\\","\\\\"))
                    .Select(it=>it.Replace("\"","\\\""))
                    .Select(it => "\"" + it +"\\r\\n"+ "\"")
                    .ToArray();
                string content = string.Join("\r\n+",lines);
                string hint = $"MyAdditionalFiles.{nameAndContent.name}";
                var str = $@"
    public static partial class MyAdditionalFiles
    {{
        public const string {nameString} = {content};
    }}";
                spc.AddSource(hint,str );
            });
        }
    }
}