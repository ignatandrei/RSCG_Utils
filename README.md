# RSCG_Utils

Roslyn Source Code Generators Utils

# Usage

## Additional Files

In your csproj

```xml
<ItemGroup>
 	  <PackageReference Include="rscgutils" Version="2023.501.2230" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
</ItemGroup>
<ItemGroup>
	<AdditionalFiles Include="Second.gen.txt" />
	<AdditionalFiles Include="first.gen.txt" />
	<AdditionalFiles Include="test\Afirst.gen.txt" />
</ItemGroup>
```

In the code

```
string x= MyAdditionalFiles.Second_gen_txt;
```

