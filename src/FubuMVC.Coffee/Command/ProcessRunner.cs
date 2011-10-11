using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using FubuCore;

namespace FubuMVC.Coffee.Command
{
    public class ProcessRunner : IProcessRunner
    {
        public ProcessReturn Run(ProcessStartInfo info, TimeSpan waitDuration, Action<string> callback)
        {
            //use the operating system shell to start the process
            //this allows credentials to flow through.
            //info.UseShellExecute = true; 

            info.UseShellExecute = false;
            info.Verb = "runas";
            info.WindowStyle = ProcessWindowStyle.Normal;
            info.CreateNoWindow = false;
            info.RedirectStandardError = info.RedirectStandardOutput = true;

            ProcessReturn returnValue;
            var output = new StringBuilder();
            using (var proc = Process.Start(info))
            {
                var pid = proc.Id;
                proc.OutputDataReceived += (sender, outputLine) =>
                {
                    if (outputLine.Data.IsNotEmpty())
                    {
                        callback(outputLine.Data);
                    }
                    output.AppendLine(outputLine.Data);
                };

                proc.BeginOutputReadLine();
                proc.WaitForExit((int)waitDuration.TotalMilliseconds);

                killProcessIfItStillExists(pid);

                returnValue = new ProcessReturn
                {
                    ExitCode = proc.ExitCode,
                    OutputText = output.ToString()
                };
            }

            return returnValue;
        }

        private static void killProcessIfItStillExists(int pid)
        {
            if (Process.GetProcesses().Where(p => p.Id == pid).Any())
            {
                try
                {
                    var p = Process.GetProcessById(pid);
                    if (!p.HasExited)
                    {
                        p.Kill();
                        Thread.Sleep(100);
                    }
                }
                catch (ArgumentException) {}
            }
        }

        public ProcessReturn Run(ProcessStartInfo info, Action<string> callback)
        {
            return Run(info, new TimeSpan(0, 0, 0, 10), callback);
        }
    }
}