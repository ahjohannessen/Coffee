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
        private readonly string _tools = FileSystem.Combine(AppDomain.CurrentDomain.BaseDirectory, Resources.ToolsName);

        public CoffeeRunner()
        {
            var cmd = "coffee.{0}".ToFormat(Platform.IsUnix() ? "sh" : "cmd");
            _coffeeCmd = _tools.AppendPath(cmd);
        }

        public RunResult Run(CoffeeRequest request)
        {
            using (var process = startCoffee(buildArgs(request)))
            {
                process.WaitForExit();
                return new RunResult(process.ExitCode);
            }
        }

        private Process startCoffee(params string[] args)
        {            
            var startInfo = new ProcessStartInfo(_coffeeCmd)
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
            args.Append(FileEscape(files.CoffeeScript));
            args.Append(" ");
            args.Append(FileEscape(files.Error));

            return args.ToString();
        }
		
		public static string FileEscape(string file)
        {	 	
			return "\"{0}\"".ToFormat(file);	
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


    public class Platform
    {
        public static bool IsUnix()
        {
            var pf = Environment.OSVersion.Platform;
            return pf == PlatformID.Unix || pf == PlatformID.MacOSX;
        }
    }
}