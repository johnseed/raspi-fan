using System.Diagnostics;
public static class CommandHelper
{
    // https://stackoverflow.com/questions/40764172/how-asp-net-core-execute-linux-shell-command
    public static string Execute(string command, string args)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (string.IsNullOrEmpty(error)) { return output; }
        else { return error; }
    }
}