using System;
using System.Diagnostics;
using FubuCore;
using FubuCore.CommandLine;

namespace FubuMVC.Coffee.Command
{
    public static class CLIRunner
    {
        public static string FileEscape(this string file)
        {
            return "\"{0}\"".ToFormat(file);
        }

        public static void RunNode(string command, params object[] parameters)
        {
            var node = getNode();

            runProcess(new ProcessStartInfo
            {
                FileName = node,               
                Arguments = command.ToFormat(parameters)
            });
        }

        // temp
        private static string getNode()
        {
            return AppDomain.CurrentDomain.BaseDirectory.AppendPath("node");
        }

        private static void runProcess(ProcessStartInfo processStartInfo)
        {
            var runner = new ProcessRunner();
            var start = Console.ForegroundColor;
            Console.WriteLine();

            ConsoleWriter.PrintHorizontalLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} {1}", processStartInfo.FileName, processStartInfo.Arguments);
            ConsoleWriter.PrintHorizontalLine();

            var returnValue = runner.Run(processStartInfo, new TimeSpan(0, 1, 0), text => { });
            var color = returnValue.ExitCode == 0 ? ConsoleColor.Gray : ConsoleColor.Red;

            Console.ForegroundColor = color;

            Console.WriteLine(returnValue.OutputText);
            Console.WriteLine("ExitCode:  " + returnValue.ExitCode);

            ConsoleWriter.PrintHorizontalLine();
            Console.ForegroundColor = start;
        }
    }
}