using System.Collections.Generic;
using Coffee;
using FubuMVC.Core.Assets.Content;
using FubuMVC.Core.Assets.Files;

namespace FubuMVC.Coffee
{
    public class CoffeeTransformer : ITransformer
    {
        private readonly ICoffee _coffee;

        public CoffeeTransformer(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public string Transform(string contents, IEnumerable<AssetFile> files)
        {
            return _coffee.Compile(contents);
        }
    }
}