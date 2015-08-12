namespace Battleships.Models
{
    using System;

    using Battleships.Common;

    public struct Position
    {
        private int row;
        private int col;

        public Position(int row, int col)
            : this()
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row
        {
            get
            {
                return this.row;
            }

            set
            {
                this.ValidateRow(value);
                this.row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }

            set
            {
                this.ValidateCol(value);
                this.col = value;
            }
        }

        public static Position GetFromBattleshipBoard(char gridRow, string gridCol)
        {
            int row = (int)gridRow - GlobalConstants.MinRowValueOnGridAsciiCode;
            int col = int.Parse(gridCol) - 1;
            var position = new Position(row, col);

            return position;
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
