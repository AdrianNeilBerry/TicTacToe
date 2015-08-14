using System.Threading;
using TicTacToe.Engine.Writer;

namespace TicTacToe.Engine.Output
{
    public class OutputAdapter : IOutputAdapter
    {
        private readonly IOutputWriter _writer;

        public OutputAdapter(IOutputWriter writer)
        {
            _writer = writer;
        }

        public void Clear()
        {
            _writer.Clear();
        }

        public void WriteBoard(char[] currentState)
        {
            _writer.WriteLine("     |     |      ");
            _writer.WriteLine($"  {currentState[0]}  |  {currentState[1]}  |  {currentState[2]}   ");
            _writer.WriteLine("_____|_____|_____ ");
            _writer.WriteLine("     |     |      ");
            _writer.WriteLine($"  {currentState[3]}  |  {currentState[4]}  |  {currentState[5]}   ");
            _writer.WriteLine("_____|_____|_____ ");
            _writer.WriteLine("     |     |      ");
            _writer.WriteLine($"  {currentState[6]}  |  {currentState[7]}  |  {currentState[8]}   ");
            _writer.WriteLine("     |     |      ");
        }

        public void LastMove(string text)
        {
            _writer.WriteLine("\n" + text);
        }

        public void WriteGameDescription(string firstPlayerName, string secondPlayerName)
        {
            _writer.WriteLine($"{firstPlayerName} is playing X, {secondPlayerName} is playing O\n");
        }

        public void StartGamePrompt()
        {
            _writer.WriteLine("\nThe atmosphere is tense, press any key to start the game...");
            _writer.ReadKey();
        }

        public void WriteGameOver(bool winner, string name)
        {
            _writer.WriteLine(winner ? $"\n{name} wins..." : "\nIt's a draw...");
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public bool StartAnotherGame()
        {
            _writer.WriteLine("\nStart another game?");
            var ch = _writer.ReadKey();
            if (ch == 'Y' || ch == 'y') return true;
            return false;
        }
    }
}