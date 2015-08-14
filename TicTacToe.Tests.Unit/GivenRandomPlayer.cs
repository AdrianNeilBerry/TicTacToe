using System.Collections.Generic;
using TicTacToe.Engine.Player;
using Xunit;

namespace TicTacToe.Tests.Unit
{
    public class GivenRandomPlayer
    {
        [Fact]
        public void WhenRecursivelyGivenInputList_ThenItShouldReferenceEveryItemInThatListOnlyOnce()
        {
            // Arrange
            var input = new[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
            var player = new RandomComputerPlayer();

            // Act
            var result = new List<int>();
            for (var i = 0; i < input.Length; i++)
            {
                result.Add(player.MakeMove(input));
            }

            // Assert
            result.Sort();
            var expected = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8};
        }
    }
}
