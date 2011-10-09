﻿using CoffeeSharp;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Coffee.Tests
{
    public class CoffeeTransformerTester : InteractionContext<CoffeeTransformer>
    {
        [Test]
        public void smoke()
        {
            var cse = new CoffeeScriptEngine();
            Assert.Pass(cse.Compile("smoke = (x) -> x * x"));
        }
    }
}