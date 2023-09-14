
Add one or more  .gen. files ( like .gen.txt ) to the project.

In your csproj

```xml
<ItemGroup>
 	  <PackageReference Include="rscgutils" Version="2023.914.2016" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
</ItemGroup>
<ItemGroup>
	<AdditionalFiles Include="Second.gen.txt" />
	<AdditionalFiles Include="first.gen.txt" />
	<AdditionalFiles Include="test\Afirst.gen.txt" />
	<AdditionalFiles Include="sql/**/*" />
</ItemGroup>
```

In the code

```csharp
//see https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string
string x= MyAdditionalFiles.Second_gen_txt;
```



If you have an additional json file, you can have a file from this

```xml
<ItemGroup>
	<AdditionalFiles Include="my.gen.json" />
</ItemGroup>
```

And you can have from the code
```csharp

var json = System.Text.Json.JsonSerializer
    .Deserialize<GeneratedJson.my_gen_json>(MyAdditionalFiles.my_gen_json);

ArgumentNullException.ThrowIfNull( json );
Console.WriteLine( ":hosts"+json.AllowedHosts );

```

To debug, you can add into the .csproj
```xml
<PropertyGroup>
	<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
	<CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)\GeneratedX</CompilerGeneratedFilesOutputPath>
</PropertyGroup>
```

Also, memoization of the return of the functions. Just add suffix _MemoPure


More details at http://msprogrammer.serviciipeweb.ro/2023/05/08/file-to-csharp-const/


