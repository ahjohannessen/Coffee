using System;

namespace Coffee
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            Resources.Extract();

            var engine = new CoffeeEngine();
            
            Console.WriteLine(engine.Compile(r => r.CoffeeCode = "smoke = (x) -> x * x"));
            
            Console.WriteLine(engine.Compile(r =>
            {
                r.CoffeeCode = "bare = (x) -> x * x";
                r.BareMode = true;
            }));

            Console.ReadKey();
        }
    }
}
