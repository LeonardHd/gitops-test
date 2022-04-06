using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace import_gitops
{
    public class Shell
    {
        public static string Run(string command, string arguments)
        {
            string? output;

            using (Process p = new Process())
            {
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = command;
                p.StartInfo.Arguments = arguments;
                p.Start();
                // Do not wait for the child process to exit before
                // reading to the end of its redirected stream.
                // p.WaitForExit();
                // Read the output stream first and then wait.
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

            }
            return output;
        }
    }
}
