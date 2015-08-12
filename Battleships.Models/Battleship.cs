namespace Battleships.Models
{
    using Battleships.Common;
    using Battleships.Models.Contracts;

    public class Battleship : Ship, IShip
    {
        public Battleship(ShipDirection direction)
            : base(GlobalConstants.BattleshipSize, direction, GlobalConstants.DestroyerSymbol)
        {
        }

        public Battleship(ShipDirection direction, Position topLeft)
            : base(GlobalConstants.BattleshipSize, direction, GlobalConstants.BattleshipSymbol, topLeft)
        {
        }
    }
}
