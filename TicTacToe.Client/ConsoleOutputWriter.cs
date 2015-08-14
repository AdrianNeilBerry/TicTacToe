using System;
using TicTacToe.Engine.Output;
using TicTacToe.Engine.Writer;

namespace TicTacToe.Client
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Clear()
        {
            Console.Clear();
        }

        public char ReadKey()
        {
            var ch = Console.ReadKey().KeyChar;
            return ch;
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}