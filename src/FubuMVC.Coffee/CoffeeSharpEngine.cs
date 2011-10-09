using CoffeeSharp;

namespace FubuMVC.Coffee
{
    public class CoffeeSharpEngine : ICoffee
    {
        private readonly CoffeeScriptEngine _engine;
        public CoffeeSharpEngine(CoffeeScriptEngine engine /*, settings */)
        {
            _engine = engine;
        }

        public string Compile(string code)
        {
            return _engine.Compile(code);
        }
    }
}