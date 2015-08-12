namespace Battleships.Models
{
    using System;

    using Battleships.Common;
    using Battleships.Models.Contracts;

    public class Grid
    {
        private readonly char[,] grid;

        public Grid(int rows = GlobalConstants.GridRowsCount, int cols = GlobalConstants.GridColsCount)
        {
            this.TotalRows = rows;
            this.TotalCols = cols;
            this.grid = new char[rows, cols];
        }

        public int TotalRows { get; private set; }

        public int TotalCols { get; private set; }

        public void PlaceShip(IShip ship)
        {
            int shipRow = ship.TopLeft.Row;
            int shipCol = ship.TopLeft.Col;

            for (int i = 0; i < ship.Size; i++)
            {
                this.grid[shipRow, shipCol] = ship.Image;

                if (ship.Direction == ShipDirection.Vertical)
                {
                    shipRow++;
                }
                else
                {
                    shipCol++;
                }
            }
        }

        public char GetCell(Position position)
        {
            return this.grid[position.Row, position.Col];
        }

        public char GetCell(int row, int col)
        {
            return this.grid[row, col];
        }

        public void SetCell(Position position, char value)
        {
            this.grid[position.Row, position.Col] = value;
        }

        public void SetCell(int row, int col, char value)
        {
            this.ValidateRow(row);
            this.ValidateCol(col);

            this.grid[row, col] = value;
        }

        private void ValidateRow(int value)
        {
            Validator.CheckIfInRange(value, 0, GlobalConstants.GridRowsCount, GlobalConstants.InvalidRowMsg);
        }

        private void ValidateCol(int value)
        {
            Validator.CheckIfInRange(value, 0, GlobalConstants.GridColsCount, GlobalConstants.InvalidColMsg);
        }
    }
}
