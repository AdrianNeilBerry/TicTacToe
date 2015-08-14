using System.Collections.Generic;

namespace TicTacToe.Engine.Engine
{
    public interface IGameEngine
    {
        List<int> Moves { get; set; }
    }
}