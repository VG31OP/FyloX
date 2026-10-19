// Entry point of fylox-cli.exe (run as "fylox" through cli\fylox.cmd). Ctrl+C cancels the running conversion or
// stops watching; a second Ctrl+C ends the process at once.

using System.Text;
using FyloX.Cli;

Console.OutputEncoding = Encoding.UTF8;
using var cancel = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    if (cancel.IsCancellationRequested)
        return;
    e.Cancel = true;
    cancel.Cancel();
};
return await Cli.RunAsync(args, Console.Out, Console.Error, cancel.Token);
