using Coffee;
using FubuMVC.Core;
using FubuMVC.Core.Assets.Content;

namespace FubuMVC.Coffee
{
    public class CoffeeExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            var coffeePolicy = JavascriptTransformerPolicy<CoffeeTransformer>
                .For(ActionType.Transformation, ".coffee");
            
            registry.Services(s =>
            {
                s.SetServiceIfNone<ICoffee, CoffeeSharpEngine>();
                s.AddService<ITransformerPolicy>(coffeePolicy);
            });
        }

        /* DSL for basic options */
    }
}
