// See https://aka.ms/new-console-template for more information
using RSCG_Utils_Console;
using System.Text.Json;
fibTest f = new();
Console.WriteLine(DateTime.Now.ToString("mm_ss"));
Console.WriteLine("no memo :"+await f.fib(5));
Console.WriteLine(DateTime.Now.ToString("mm_ss"));
Console.WriteLine("memo :" + await f.fibonacci(5));
Console.WriteLine(DateTime.Now.ToString("mm_ss"));
Console.WriteLine("FAST memo :" + await f.fibonacci(5));
Console.WriteLine(DateTime.Now.ToString("mm_ss"));

Console.WriteLine("first time :" + f.Test());
Console.WriteLine("second time :" + f.Test());


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

//var newJson = System.Text.Json.JsonSerializer
//    .Deserialize<NS_GeneratedJson_my_gen_json.Simple_gen_json>
//    (MyAdditionalFiles.Simple_gen_json);

//ArgumentNullException.ThrowIfNull(newJson);

//Console.WriteLine(":hosts" + newJson.AllowedHosts);


var json = System.Text.Json.JsonSerializer
    .Deserialize<NS_GeneratedJson_my_gen_json.my_gen_json>(MyAdditionalFiles.my_gen_json);

ArgumentNullException.ThrowIfNull(json);
Console.WriteLine(":hosts" + json.AllowedHosts);
Console.WriteLine(json.Logging?.LogLevel?.Microsoft);

var newJson = NS_GeneratedJson_Simple_gen_json.Simple_gen_json.Deserialize(MyAdditionalFiles.Simple_gen_json);
Console.WriteLine(":hosts" + newJson?.AllowedHosts);
