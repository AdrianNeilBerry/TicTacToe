using System.Collections.Generic;
using System.Linq;
using Moq;
using TicTacToe.Engine;
using TicTacToe.Engine.Engine;
using TicTacToe.Engine.Output;
using TicTacToe.Engine.Player;
using TicTacToe.Engine.Writer;
using Xunit;

namespace TicTacToe.Tests.Unit
{
    public class GivenGameOfTicTacToe
    {
        private readonly RandomComputerPlayer _firstPlayer = new RandomComputerPlayer {Name = "Deep Blue", Banter = "\nDeep Blue Strikes..." };
        private readonly RandomComputerPlayer _secondPlayer = new RandomComputerPlayer {Name = "Garry Kasparov", Banter = "\nKasparov Retaliates..." };

        [Fact]
        public void WhenTheApplicationIsStarted_ThenItShouldDrawTheInitialInterface()
        {
            // Arrange
            var screenOutput = new List<string>();

            var writer = new Mock<IOutputWriter>();
            var outputAdapter = new OutputAdapter(writer.Object);
            writer.Setup(output => output.WriteLine(It.IsAny<string>())).Callback((string s) => screenOutput.Add(s));

            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter, ticTacToeEngine, _firstPlayer, _secondPlayer);
            
            // Act
            game.Start();

            // Assert
            Assert.Equal("Deep Blue is playing X, Garry Kasparov is playing O\n", screenOutput.ElementAt(0));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(1));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(2));
            Assert.Equal("_____|_____|_____ ", screenOutput.ElementAt(3));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(4));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(5));
            Assert.Equal("_____|_____|_____ ", screenOutput.ElementAt(6));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(7));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(8));
            Assert.Equal("     |     |      ", screenOutput.ElementAt(9));
            Assert.Equal("\nThe atmosphere is tense, press any key to start the game...", screenOutput.ElementAt(10));
            // Unusual for me to be checking view content like this, would normally use a tool like selenium in a web environment
            // Unusual for me to be checking multiple things with positioning, however it seems the logical choice in this instance
        }

        [Fact]
        public void WhenTheApplicationIsStarted_ThenItShouldPromptTheUserToStart()
        {
            // Arrange
            var outputAdapter = new Mock<IOutputAdapter>();
            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter.Object, ticTacToeEngine, _firstPlayer, _secondPlayer);

            // Act
            game.Start();

            // Assert
            outputAdapter.Verify(adapter => adapter.StartGamePrompt(), Times.Once);
        }

        [Fact]
        public void WhenPlayStarts_ThePlayShouldAlternateBetweenPlayers()
        {
            // Arrange
            var writer = new Mock<IOutputWriter>();
            var outputAdapter = new OutputAdapter(writer.Object);
            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter, ticTacToeEngine, _firstPlayer, _secondPlayer);

            // Act
            game.Start();

            // Assert
            var firstPlayerMoves = ticTacToeEngine.Moves.Where((item, index) => index % 2 == 0);
            Assert.True(firstPlayerMoves.SequenceEqual(_firstPlayer.Moves));

            var secondPlayerMoves = ticTacToeEngine.Moves.Where((item, index) => index % 2 != 0);
            Assert.True(secondPlayerMoves.SequenceEqual(_secondPlayer.Moves));
        }

        [Fact]
        public void WhenMovesAreMade_ThenItShouldUpdateTheBoard()
        {
            // Arrange
            var nonMoveRelatedBoardRefreshes = 1;
            var boardRefreshes = 0;
            var outputAdapter = new Mock<IOutputAdapter>();
            outputAdapter.Setup(adapter => adapter.WriteBoard(It.IsAny<char[]>())).Callback(() => boardRefreshes++);
            
            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter.Object, ticTacToeEngine, _firstPlayer, _secondPlayer);

            // Act
            game.Start();

            // Assert
            Assert.Equal(ticTacToeEngine.Moves.Count + nonMoveRelatedBoardRefreshes, boardRefreshes);
        }

        [Fact]
        public void WhenMovesAreMade_ThenItShouldWaitForOneSecond()
        {
            // Arrange
            var waitCount = 0;
            var outputAdapter = new Mock<IOutputAdapter>();
            outputAdapter.Setup(adapter => adapter.Wait(It.IsAny<int>())).Callback(() => waitCount++);
            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter.Object, ticTacToeEngine, _firstPlayer, _secondPlayer, 1000);

            // Act
            game.Start();

            // Assert
            Assert.Equal(ticTacToeEngine.Moves.Count , waitCount);
        }

        [Fact]
        public void WhenTheGameIsOver_ThenItShouldDisplayTheResultOfTheGame()
        {
            // Arrange
            var outputAdapter = new Mock<IOutputAdapter>();
            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter.Object, ticTacToeEngine, _firstPlayer, _secondPlayer);

            // Act
            game.Start();

            // Assert
            outputAdapter.Verify(adapter => adapter.WriteGameOver(It.IsAny<bool>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WhenTheGameIsOver_ThenItShouldOfferToStartAnotherGame()
        {
            // Arrange
            var outputAdapter = new Mock<IOutputAdapter>();
            var ticTacToeEngine = new TicTacToeEngine();
            var game = new Game(outputAdapter.Object, ticTacToeEngine, _firstPlayer, _secondPlayer);

            // Act
            game.Start();

            // Assert
            outputAdapter.Verify(adapter => adapter.StartAnotherGame(), Times.Once);
        }
    }
}
