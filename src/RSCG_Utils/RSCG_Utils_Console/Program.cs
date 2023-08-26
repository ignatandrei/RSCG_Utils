// See https://aka.ms/new-console-template for more information
using System.Text.Json;

Console.WriteLine("Hello, World!");
Console.WriteLine(MyAdditionalFiles.first_gen_txt);
Console.WriteLine(MyAdditionalFiles.Second_gen_txt);
Console.WriteLine(MyAdditionalFiles.Afirst_gen_txt);
Console.WriteLine(MyAdditionalFiles.test_a_b_gen_sql);


var x = """""""
this is a string with quotes "
and here is another with  slash \
and without slash
""""""";
Console.WriteLine(x);

var newJson = System.Text.Json.JsonSerializer.Deserialize<GeneratedJson.Simple_gen_json>(MyAdditionalFiles.Simple_gen_json);
ArgumentNullException.ThrowIfNull(newJson);
Console.WriteLine(":hosts" + newJson.AllowedHosts);


var json = System.Text.Json.JsonSerializer
    .Deserialize<GeneratedJson.my_gen_json>(MyAdditionalFiles.my_gen_json,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    }
    );

ArgumentNullException.ThrowIfNull( json );
Console.WriteLine( ":hosts"+json.AllowedHosts );
//Console.WriteLine(json.Logging.LogLevel.Microsoft);
