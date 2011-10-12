using System;
using System.Collections.Generic;
using System.Diagnostics;
using FubuCore;
using FubuCore.CommandLine;

namespace Coffee.Command
{
    public static class CoffeeRunner
    {
        public static void Run(string command, params string[] parameters)
        {
            runProcess(new ProcessStartInfo
            {
                FileName = qualifyCommand(command),               
                Arguments = parameters.Join(" ")
            });
        }

        // temp
        private static string qualifyCommand(string cmd)
        {
            return AppDomain.CurrentDomain.BaseDirectory.AppendPath("tools", cmd);
        }

        public static string FileEscape(this string file)
        {
            return "\"{0}\"".ToFormat(file);
        }

        private static void runProcess(ProcessStartInfo processStartInfo)
        {
            var runner = new ProcessRunner();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} {1}", processStartInfo.FileName, processStartInfo.Arguments);

            var returnValue = runner.Run(processStartInfo, new TimeSpan(0, 1, 0), text =>
            {
                  ConsoleWriter.WriteWithIndent(ConsoleColor.DarkYellow, 4, text);                                                                        
            });
            var color = returnValue.ExitCode == 0 ? ConsoleColor.Gray : ConsoleColor.Red;

            Console.ForegroundColor = color;
            Console.WriteLine(returnValue.OutputText);
            Console.WriteLine("ExitCode:  " + returnValue.ExitCode);
        }       
    }
}