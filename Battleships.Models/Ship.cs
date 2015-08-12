namespace Battleships.Models
{
    using System;

    using Battleships.Common;
    using Battleships.Models.Contracts;

    public abstract class Ship : IShip
    {
        private int size;

        public Ship(int size, ShipDirection direction, char shipSymbol)
        {
            this.Size = size;
            this.Direction = direction;
            this.Image = shipSymbol;
        }

        public Ship(int size, ShipDirection direction, char shipSymbol, Position topLeft)
            : this(size, direction, shipSymbol)
        {
            this.TopLeft = topLeft;
        }

        public int Size
        {
            get
            {
                return this.size;
            }

            set
            {
                this.ValidateSize(value);
                this.size = value;
            }
        }

        public ShipDirection Direction { get; set; }

        public char Image { get; set; }

        public Position TopLeft { get; set; }

        public bool IsSunk { get; set; }

        public int HitsCount { get; set; }

        private void ValidateSize(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException(GlobalConstants.ShipNegativeSizeMsg);
            }
        }
    }
}
