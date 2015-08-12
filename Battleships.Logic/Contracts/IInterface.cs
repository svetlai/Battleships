namespace Battleships.Logic.Contracts
{
    using Battleships.Models;

    public interface IInterface
    {
        string GetUserInput();

        UserCommand GetCommandFromInput();

        Position GetShotPositionFromInput();

        void ExitGame();
    }
}
