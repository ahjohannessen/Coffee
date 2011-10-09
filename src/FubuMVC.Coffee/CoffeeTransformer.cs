using System.Collections.Generic;
using FubuMVC.Core.Assets.Content;
using FubuMVC.Core.Assets.Files;
using FubuMVC.Core.Diagnostics;

namespace FubuMVC.Coffee
{
    public class CoffeeTransformer : ITransformer
    {
        private readonly ICoffee _coffee;
        private readonly IDebugDetector _debugDetector;

        public CoffeeTransformer(ICoffee coffee, IDebugDetector debugDetector)
        {
            _coffee = coffee;
            _debugDetector = debugDetector;
        }

        public string Transform(string contents, IEnumerable<AssetFile> files)
        {
            if(_debugDetector.IsDebugCall())
            {
                /* Log */      
            }

            return _coffee.Compile(contents);
        }
    }
}