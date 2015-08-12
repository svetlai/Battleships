namespace BattleShips.Console
{
    using System;
    using System.Text;

    using Battleships.Common;
    using Battleships.Models;
    using BattleShips.Logic.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        private const int GridRowPosition = 2;
        private const int GridColPosition = 0;

        private StringBuilder scene;

        public ConsoleRenderer()
        {
            this.scene = new StringBuilder();
        }

        public void RenderGrid(Grid grid)
        {
            Console.SetCursorPosition(GridColPosition, GridRowPosition);

            this.scene.Clear()
                .Append(' ');

            for (int i = 1; i <= GlobalConstants.GridColsCount; i++)
            {
                string toAppend = i < 9 ? i.ToString() : i.ToString().Substring(i.ToString().Length - 1);
                this.scene.Append(toAppend);
            }

            this.scene.AppendLine();

            for (int row = 0; row < grid.TotalRows; row++)
            {
                this.scene.Append((char)(GlobalConstants.MinRowValueOnGridAsciiCode + row));

                for (int col = 0; col < grid.TotalCols; col++)
                {
                    this.scene.Append(grid.GetCell(row, col));
                }

                this.scene.Append(Environment.NewLine);
            }

            Console.WriteLine(this.scene.ToString());
            this.SetCursorAtInputPosition();
        }

        /// <summary>
        /// Updates grid position
        /// </summary>
        public void UpdateGrid(Grid grid, Position position)
        {
            Console.SetCursorPosition(position.Col + GridColPosition + 1, position.Row + GridRowPosition + 1);
            Console.Write(grid.GetCell(position.Row, position.Col));
            this.SetCursorAtInputPosition();
        }

        public void RenderMessage(string message, bool setCursor = true)
        {
            this.ClearRow(GridRowPosition + GlobalConstants.GridRowsCount + 2);
            Console.SetCursorPosition(0, GridRowPosition + GlobalConstants.GridRowsCount + 2);
            Console.WriteLine(message);
            if (setCursor)
            {
                this.SetCursorAtInputPosition();
            }
        }

        public void RenderStatusMessage(string message)
        {
            this.ClearRow(0);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("<< {0} >>", message);
            this.SetCursorAtInputPosition();
        }

        public void RenderErrorMessage(string message)
        {
            this.ClearRow(GridRowPosition + GlobalConstants.GridRowsCount + 4);
            Console.SetCursorPosition(0, GridRowPosition + GlobalConstants.GridRowsCount + 4);
            Console.WriteLine(message);
            this.SetCursorAtInputPosition();
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void ClearError()
        {
            this.ClearRow(GridRowPosition + GlobalConstants.GridRowsCount + 4);
        }

        private void ClearRow(int row)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, row);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        private void SetCursorAtInputPosition()
        {
            this.ClearRow(GridRowPosition + GlobalConstants.GridRowsCount + 3);
            Console.SetCursorPosition(0, GridRowPosition + GlobalConstants.GridRowsCount + 3);
        }
    }
}
