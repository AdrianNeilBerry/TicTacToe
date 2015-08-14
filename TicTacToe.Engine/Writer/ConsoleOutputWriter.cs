using System;

namespace TicTacToe.Engine.Writer
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Clear()
        {
            Console.Clear();
        }

        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}