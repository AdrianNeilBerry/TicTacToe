using TicTacToe.Engine;
using TicTacToe.Engine.Engine;
using TicTacToe.Engine.Output;
using TicTacToe.Engine.Player;

namespace TicTacToe.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstPlayer = new RandomComputerPlayer {Name = "Fred", Banter = "Fred makes his move..."};
            var secondPlayer = new RandomComputerPlayer {Name = "Joe", Banter = "Joe strikes back..."};
            new Game(new OutputAdapter(new ConsoleOutputWriter()), new TicTacToeEngine(), firstPlayer, secondPlayer, 1000).Start();
        }
    }
}
