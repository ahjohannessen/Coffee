using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FubuCore;

namespace Coffee
{
    public interface ICoffeeRunner
    {
        RunResult Run(CoffeeRequest request);
    }

    public class CoffeeRunner : ICoffeeRunner
    {
        private readonly string _coffeeCmd;
        
        public CoffeeRunner() : this(FileSystem.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools", "coffee.cmd")) {}
        public CoffeeRunner(string coffeeCmd)
        {
            _coffeeCmd = coffeeCmd;
        }

        public RunResult Run(CoffeeRequest request)
        {
            using (var process = startCoffee(_coffeeCmd, buildArgs(request)))
            {
                process.WaitForExit();
                return new RunResult(process.ExitCode);
            }
        }

        private static Process startCoffee(string cmd, params string[] args)
        {            
            var startInfo = new ProcessStartInfo(cmd)
            {
                Arguments = args.Join(" "),
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            return Process.Start(startInfo);
        }

        private static string buildArgs(CoffeeRequest request)
        {
            var files = request.RunFiles;

            var args = new StringBuilder();
            args.Append("-c");
            if (request.BareMode)
            {
                args.Append("b");
            }

            args.Append(" ");
            args.Append(files.CoffeeScript);
            args.Append(" ");
            args.Append(files.Error);

            return args.ToString();
        }
    }

    public class RunResult
    {
        private readonly int _exitCode;
        public RunResult(int exitCode)
        {
            _exitCode = exitCode;
        }

        public bool Success
        {
            get { return _exitCode == 0; }
        }
    }
}