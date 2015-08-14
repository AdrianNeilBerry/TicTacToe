using System.Collections.Generic;

namespace TicTacToe.Engine.Engine
{
    public class TicTacToeEngine : IGameEngine
    {
        public List<int> Moves { get; set; } = new List<int>();
    }
}