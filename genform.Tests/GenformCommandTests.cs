using System.Diagnostics;
using System.IO;

namespace genform.Tests;

public class GenformCommandTests
{
    private static (int ExitCode, string Output) RunGenform(params string[] arguments)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var genformProj = Path.Combine(projectRoot, "genform");
        var psi = new ProcessStartInfo("dotnet")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        psi.ArgumentList.Add("run");
        psi.ArgumentList.Add("--project");
        psi.ArgumentList.Add(genformProj);
        psi.ArgumentList.Add("--");
        foreach (var arg in arguments)
        {
            psi.ArgumentList.Add(arg);
        }

        using var process = Process.Start(psi)!;
        process.WaitForExit();
        var output = process.StandardOutput.ReadToEnd() + process.StandardError.ReadToEnd();
        return (process.ExitCode, output);
    }

    [Fact]
    public void RunsWithModelAndOutput()
    {
        var (code, output) = RunGenform("-model", "MyModel.dll", "-output", "out");
        Assert.Equal(0, code);
        Assert.Contains("Model: MyModel.dll", output);
        Assert.Contains("Output: out", output);
    }

    [Fact]
    public void FailsWithOnlyModel()
    {
        var (_, output) = RunGenform("-model", "MyModel.dll");
        Assert.Contains("Usage: genform -model <assembly> -output <folder>", output);
    }

    [Fact]
    public void FailsWithOnlyOutput()
    {
        var (_, output) = RunGenform("-output", "out");
        Assert.Contains("Usage: genform -model <assembly> -output <folder>", output);
    }
}
