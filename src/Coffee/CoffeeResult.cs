namespace Coffee
{
    public class CoffeeResult
    {
        public bool Success { get; set; }
        public string TransformedCode { get; set; }
        public string Error { get; set; }

        public override string ToString()
        {
            return Success ? TransformedCode : Error;
        }
    }
}