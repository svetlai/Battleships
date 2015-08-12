namespace BattleShips.Logic.Contracts
{
    using Battleships.Models;

    public interface IRenderer
    {
        void RenderGrid(Grid grid);

        void UpdateGrid(Grid grid, Position position);
       
        void RenderMessage(string message, bool setCursor = true);

        void RenderStatusMessage(string message);

        void RenderErrorMessage(string message);

        void Clear();

        void ClearError();
    }
}
