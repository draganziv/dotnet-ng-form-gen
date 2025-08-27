using System;

if (args.Length == 0)
{
    Console.WriteLine("Usage: genform -model <assembly> -output <folder>");
    return;
}

string? model = null;
string? output = null;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "-model":
            if (i + 1 < args.Length)
            {
                model = args[++i];
            }
            else
            {
                Console.WriteLine("Error: -model requires a value.");
                return;
            }
            break;
        case "-output":
            if (i + 1 < args.Length)
            {
                output = args[++i];
            }
            else
            {
                Console.WriteLine("Error: -output requires a value.");
                return;
            }
            break;
        default:
            Console.WriteLine($"Unknown argument: {args[i]}");
            return;
    }
}

if (model is null || output is null)
{
    Console.WriteLine("Usage: genform -model <assembly> -output <folder>");
    return;
}

Console.WriteLine($"Model: {model}");
Console.WriteLine($"Output: {output}");
