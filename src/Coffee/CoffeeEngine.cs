using System;

namespace Coffee
{
    public interface ICoffeeEngine
    {
        CoffeeResult Compile(Action<CoffeeRequest> configure);
    }

    public class CoffeeEngine : ICoffeeEngine
    {
        private readonly ICoffeeRunner _runner;

        public CoffeeEngine() : this(new CoffeeRunner()) {}
        public CoffeeEngine(ICoffeeRunner runner)
        {
            _runner = runner;
        }

        public CoffeeResult Compile(Action<CoffeeRequest> configure)
        {
            var request = new CoffeeRequest();
            configure(request);
            request.CreateCoffeeFile();
            return execute(request);
        }

        private CoffeeResult execute(CoffeeRequest request)
        {
            var coffeeResult = new CoffeeResult();
            var files = request.RunFiles;

            if(_runner.Run(request).Success)
            {
                coffeeResult.TransformedCode = files.ReadJavaScript();
                coffeeResult.Success = true;
            }
            else
            {
                coffeeResult.Error = files.ReadError();
                coffeeResult.Success = false;
            }

            files.DeleteFiles();

            return coffeeResult;
        }
    }
}