using System.IO;
using FubuCore;

namespace Coffee
{
    public class RunFiles
    {
        private readonly IFileSystem _fileSystem;
        private readonly string _uniqueName;

        public RunFiles() : this(new FileSystem()) {}
        public RunFiles(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _uniqueName = FileSystem.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            CoffeeScript = getFileName("coffee");
            JavaScript = getFileName("js");
            Error = getFileName("log");
        }

        public string CoffeeScript { get; private set; }
        public string JavaScript { get; private set; }
        public string Error { get; private set; }

        private string getFileName(string extension)
        {
            return "{0}.{1}".ToFormat(_uniqueName, extension);
        }

        public void WriteCoffee(string code)
        {
            _fileSystem.WriteStringToFile(CoffeeScript, code);
        }

        public string ReadJavascript()
        {
            return _fileSystem.ReadStringFromFile(JavaScript);
        }

        public string ReadError()
        {
            return _fileSystem.ReadStringFromFile(Error);
        }

        public void DeleteFiles()
        {
            _fileSystem.DeleteFile(CoffeeScript);
            _fileSystem.DeleteFile(JavaScript);
            _fileSystem.DeleteFile(Error);
        }
    }
}