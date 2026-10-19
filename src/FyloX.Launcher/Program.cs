using System.Diagnostics;

var target = Path.Combine(AppContext.BaseDirectory, "FyloX.exe");
if (!File.Exists(target))
{
    Console.Error.WriteLine("Error: FyloX.exe not found at " + target);
    return 1;
}

var psi = new ProcessStartInfo(target)
{
    UseShellExecute = true,
};

foreach (var arg in args)
{
    psi.ArgumentList.Add(arg);
}

try
{
    Process.Start(psi);
    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine("Failed to launch: " + ex.Message);
    return 1;
}
