namespace TicTacToe.Engine.Writer
{
    public interface IOutputWriter
    {
        void Clear();
        char ReadKey();
        void WriteLine(string s);
    }
}