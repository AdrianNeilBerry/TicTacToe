using System;
using System.Linq;
using TicTacToe.Engine.Engine;
using TicTacToe.Engine.Output;
using TicTacToe.Engine.Player;

namespace TicTacToe.Engine
{
    public class Game
    {
        private readonly IOutputAdapter _outputAdapter;
        private readonly IGameEngine _gameEngine;
        private readonly int _waitBetweenMoves;
        private char[] _squares = new char[9];
        readonly IPlayer[] _players = new IPlayer[2] ;

        public Game(IOutputAdapter outputAdapter, IGameEngine gameEngine, IPlayer firstPlayer, IPlayer secondPlayer, int waitBetweenMoves = 0)
        {
            _outputAdapter = outputAdapter;
            _gameEngine = gameEngine;
            _players[0] = firstPlayer;
            _players[1] = secondPlayer;
            _waitBetweenMoves = waitBetweenMoves;
        }

        public void Start()
        {
            InitializeBoard();
            Draw();
            _outputAdapter.StartGamePrompt();
            StartGame();
        }

        private void StartGame()
        {
            var winner = 0;
            var currentPlayer = 0;
            while (winner == 0)
            {


                var userMove = _players[currentPlayer].MakeMove(_squares);
                _gameEngine.Moves.Add(userMove);
                if (currentPlayer == 0)
                {
                    _players[0].Moves.Add(userMove);
                    _squares[userMove] = 'X';
                }
                else
                {
                    _players[1].Moves.Add(userMove);
                    _squares[userMove] = 'O';
                }
                _outputAdapter.Wait(_waitBetweenMoves);

                Draw();
                _outputAdapter.LastMove(_players[currentPlayer].Banter);

                winner = CheckForWin();
                if (winner != 0) break;

                currentPlayer = currentPlayer == 0 ? 1 : 0;
            }
            _outputAdapter.WriteGameOver(winner == 1, _players[currentPlayer].Name);
            if (_outputAdapter.StartAnotherGame())
            {
                InitializeBoard();
                StartGame();
            }
        }

        private void InitializeBoard()
        {
            for (var i = 0; i < 9; i++)
            {
                _squares[i] = ' ';
            }
        }

        private void Draw()
        {
            _outputAdapter.Clear();
            _outputAdapter.WriteGameDescription(_players[0].Name, _players[1].Name);
            _outputAdapter.WriteBoard(_squares);
        }

        private int CheckForWin()
        {
            if (HorizontalWin()) return 1;
            if (VerticalWin()) return 1;
            if (DiagonalWin()) return 1;
            if (GameDrawn()) return -1;
            return 0;
        }

        private bool GameDrawn()
        {
            return _squares.All(square => !char.IsWhiteSpace(square));
        }

        private bool DiagonalWin()
        {
            if (!IsEmpty(0) && _squares[0] == _squares[4] && _squares[4] == _squares[8])
            {
                return true;
            }
            if (!IsEmpty(2) && _squares[2] == _squares[4] && _squares[4] == _squares[6])
            {
                return true;
            }
            return false;
        }

        private bool VerticalWin()
        {
            if (!IsEmpty(0) && _squares[0] == _squares[3] && _squares[3] == _squares[6])
            {
                return true;
            }
            if (!IsEmpty(1) && _squares[1] == _squares[4] && _squares[4] == _squares[7])
            {
                return true;
            }
            if (!IsEmpty(2) && _squares[2] == _squares[5] && _squares[5] == _squares[8])
            {
                return true;
            }
            return false;
        }

        private bool HorizontalWin()
        {
            if (!IsEmpty(0) && _squares[0] == _squares[1] && _squares[1] == _squares[2])
            {
                return true;
            }
            if (!IsEmpty(3) && _squares[3] == _squares[4] && _squares[4] == _squares[5])
            {
                return true;
            }
            if (!IsEmpty(6) && _squares[6] == _squares[7] && _squares[7] == _squares[8])
            {
                return true;
            }
            return false;
        }

        private bool IsEmpty(int square)
        {
            return char.IsWhiteSpace(_squares[square]);
        }
    }
}