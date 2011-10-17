namespace Coffee
{
    public class CoffeeRequest
    {
        public CoffeeRequest() : this(new RunFiles()) {}
        public CoffeeRequest(RunFiles runFiles)
        {
            RunFiles = runFiles;
            CoffeeCode = string.Empty;
        }

        public string CoffeeCode { get; set; }
        public bool BareMode { get; set; }

        public RunFiles RunFiles { get; private set; }

        public void CreateCoffeeFile()
        {
            RunFiles.WriteCoffee(CoffeeCode);
        }
    }
}