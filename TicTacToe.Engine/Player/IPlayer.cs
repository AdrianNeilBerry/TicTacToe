using System.Collections.Generic;

namespace TicTacToe.Engine.Player
{
    public interface IPlayer
    {
        string Name { get; set; }
        string Banter { get; set; }
        List<int> Moves { get; }

        int MakeMove(char[] currentState);
    }
}