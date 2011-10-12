using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;

namespace Coffee
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            ExtractTools();

            //var test = AppDomain.CurrentDomain.BaseDirectory.AppendPath("tools", "test.coffee");

            //CoffeeRunner.Run("coffee.cmd", "-c", test);
            
            Console.ReadKey();
        }

        // Temp 
        public static void ExtractTools()
        {
            var assembly = typeof (ICoffee).Assembly;
            const string toolsName = "tools";
            var embeddedPrefix = "Coffee.{0}".ToFormat(toolsName);

            var resources = assembly.GetManifestResourceNames().Where(rn => rn.StartsWith(embeddedPrefix));
            var rootPath = AppDomain.CurrentDomain.BaseDirectory;
            var toolsPath = rootPath.AppendPath(toolsName);

            var fs = new FileSystem();

            resources.Each(r =>
            {
                if(r.EndsWith("node.exe"))
                {
                    var node = assembly.GetManifestResourceStream(r);
                    fs.WriteStreamToFile(toolsPath.AppendPath("node.exe"), node);
                    return;
                }

                if(r.EndsWith("coffee"))
                {
                    var coffee = assembly.GetManifestResourceStream(r);
                    fs.WriteStreamToFile(toolsPath.AppendPath("bin","coffee"), coffee);
                    return;
                }
                
                if(r.EndsWith(".js"))
                {
                    var js = assembly.GetManifestResourceStream(r);
                    var fileName = r.Split('.').Reverse().Take(2).Reverse().Join(".");
                    fs.WriteStreamToFile(toolsPath.AppendPath("lib","coffee-script", fileName), js);
                }
            });
        }
    }
}
