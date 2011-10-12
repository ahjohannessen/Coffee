using CoffeeSharp;

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
}