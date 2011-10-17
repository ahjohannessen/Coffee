using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;

namespace Coffee
{
    public static class Resources
    {
        public static readonly Assembly Assembly = typeof(ICoffeeEngine).Assembly;
        public static readonly string ToolsName = "tools";
        public static readonly string EmbeddedPrefix = "Coffee.{0}".ToFormat(ToolsName);

        public static void Extract()
        {
            Assembly.GetManifestResourceNames().Where(rn => rn.StartsWith(EmbeddedPrefix)).Each(name =>
            {
                if (name.EndsWith("node.exe") || name.EndsWith("coffee.cmd"))
                {
                    WriteToTools(name, name.ExtractFilename());
                    return;
                }

                if (name.EndsWith("coffee"))
                {
                    WriteToTools(name, "bin", "coffee");
                    return;
                }

                if (name.EndsWith(".js"))
                {
                    WriteToTools(name, "lib", "coffee-script", name.ExtractFilename());
                }
            });
        }

        public static void WriteToTools(string name, params string[] subpaths)
        {
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            var toolsPath = rootPath.AppendPath(ToolsName);
            var stream = Assembly.GetManifestResourceStream(name);

            var fs = new FileSystem();
            fs.WriteStreamToFile(toolsPath.AppendPath(subpaths), stream);
        }

        public static string ExtractFilename(this string mainfestName)
        {
            return mainfestName.Split('.').Reverse().Take(2).Reverse().Join(".");
        }
    }
}