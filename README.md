# RSCG_Utils

Roslyn Source Code Generators Utils

[![pack to nuget](https://github.com/ignatandrei/RSCG_Utils/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ignatandrei/RSCG_Utils/actions/workflows/dotnet.yml)

[![pack to nuget](https://img.shields.io/nuget/dt/rscgutils?style=for-the-badge)](https://www.nuget.org/packages/rscgutils)

# Usage

## Additional Files

Works with https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string

In your csproj

```xml
<ItemGroup>
 	  <PackageReference Include="rscgutils" Version="2023.502.835" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
</ItemGroup>
<ItemGroup>
	<AdditionalFiles Include="Second.gen.txt" />
	<AdditionalFiles Include="first.gen.txt" />
	<AdditionalFiles Include="test\Afirst.gen.txt" />
</ItemGroup>
```

In the code

```csharp
string x= MyAdditionalFiles.Second_gen_txt;
```

To debug, you can add into the .csproj
```xml
<PropertyGroup>
	<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
	<CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)\GeneratedX</CompilerGeneratedFilesOutputPath>
</PropertyGroup>
```

