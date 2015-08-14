using System;
using System.Collections.Generic;

namespace TicTacToe.Engine.Player
{
    public class RandomComputerPlayer : IPlayer
    {
        private readonly Random _randomNumber = new Random();

        public string Name { get; set; }
        public string Banter { get; set; }
        public List<int> Moves { get; } = new List<int>();

        public int MakeMove(char[] currentState)
        {
            var legalMove = -1;
            while (legalMove == -1)
            {
                var move = _randomNumber.Next(0, 9);
                if (currentState[move] == ' ') legalMove = move;
                // overhead here but marginal in this application
            }
            return legalMove;
        }
    }
}