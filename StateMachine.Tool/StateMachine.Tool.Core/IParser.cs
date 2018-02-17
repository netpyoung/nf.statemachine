using System.IO;

namespace StateMachine.Tool.Core
{
    public interface IParser
    {
        IGraph Parse(Stream stream);
        IGraph Parse(string fpath);
        bool TryParse(Stream stream, out IGraph graph);
        bool TryParse(string fpath, out IGraph graph);
    }
}