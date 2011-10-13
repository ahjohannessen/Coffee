using CoffeeSharp;
using FubuTestingSupport;
using NUnit.Framework;
using SassAndCoffee.Core.Compilers;

namespace FubuMVC.Coffee.Tests
{
    public class CoffeeTransformerTester : InteractionContext<CoffeeTransformer>
    {
        [Test]
        public void smoke()
        {
            // this is dog slow. consider SassAndCoffee.
            var cse = new CoffeeScriptEngine();
            Assert.Pass(cse.Compile("smoke = (x) -> x * x"));
        }

        [Test]
        public void sassy()
        {
            var compiler = new CoffeeScriptCompiler();
            Assert.Pass(compiler.Compile("smoke = (x) -> x * x"));
        }
    }
}
