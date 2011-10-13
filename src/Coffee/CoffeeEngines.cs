using CoffeeSharp;
using SassAndCoffee.Core.Compilers;

namespace Coffee
{
    public interface ICoffee
    {
        string Compile(string code);
    }

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

    public class SassCoffeeEngine : ICoffee
    {
        private readonly CoffeeScriptCompiler _coffeeScriptCompiler;

        public SassCoffeeEngine(CoffeeScriptCompiler coffeeScriptCompiler)
        {
            _coffeeScriptCompiler = coffeeScriptCompiler;
        }

        public string Compile(string code)
        {
            return _coffeeScriptCompiler.Compile(code);
        }
    }

    public class RealCoffee : ICoffee
    {
        public string Compile(string code)
        {
            throw new System.NotImplementedException();
        }
    }
}