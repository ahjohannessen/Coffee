namespace Coffee
{
    public interface ICoffeeCompiler
    {
        string Compile(string code);
    }

    public class CoffeeCompiler : ICoffeeCompiler
    {
        public string Compile(string code)
        {
            throw new System.NotImplementedException();
        }
    }
}