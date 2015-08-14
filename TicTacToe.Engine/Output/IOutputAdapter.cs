namespace TicTacToe.Engine.Output
{
    public interface IOutputAdapter
    {
        void Clear();
        void WriteBoard(char[] currentState);
        void LastMove(string text);
        void WriteGameDescription(string firstPlayerName, string secondPlayerName);
        void StartGamePrompt();
        void WriteGameOver(bool winner, string name);
        void Wait(int milliseconds);
        bool StartAnotherGame();
    }
}