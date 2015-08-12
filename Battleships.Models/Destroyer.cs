namespace Battleships.Models
{
    using Battleships.Common;
    using Battleships.Models.Contracts;

    public class Destroyer : Ship, IShip
    {
        public Destroyer(ShipDirection direction)
            : base(GlobalConstants.DestroyerSize, direction, GlobalConstants.DestroyerSymbol)
        {
        }

        public Destroyer(ShipDirection direction, Position topLeft)
            : base(GlobalConstants.DestroyerSize, direction, GlobalConstants.DestroyerSymbol, topLeft)
        {
        }
    }
}
