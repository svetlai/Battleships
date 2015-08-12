namespace BattleShips.Console
{
    using System;

    using Battleships.Common;
    using Battleships.Logic;
    using Battleships.Logic.Contracts;
    using Battleships.Models;

    public class ConsoleInterface : IInterface
    {
        private string coordinates;

        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public UserCommand GetCommandFromInput()
        {
            string input = this.GetUserInput().ToUpper();

            switch (input)
            {
                case GlobalConstants.ShowCommand:
                    return UserCommand.Show;
                case GlobalConstants.ExitCommand:
                    return UserCommand.Exit;
                case GlobalConstants.NewGameCommand:
                    return UserCommand.New;
                case GlobalConstants.AgreeCommand:
                    return UserCommand.Agree;
                default:
                    if (this.AreValidCoordinates(input))
                    {
                        this.coordinates = input;
                        return UserCommand.Shoot;
                    }

                    return UserCommand.Invalid;
            }
        }

        public Position GetShotPositionFromInput()
        {
            return Position.GetFromBattleshipBoard(this.coordinates[0], this.coordinates.Substring(1));
        }

        public void ExitGame()
        {
            Environment.Exit(0);
        }

        private bool AreValidCoordinates(string coordinates)
        {
            int col;
            if (coordinates.Length < 2 || coordinates.Length > 3 || !char.IsLetter(coordinates[0])
                || coordinates[0] < GlobalConstants.MinRowValueOnGrid || coordinates[0] > GlobalConstants.MaxRowValueOnGrid || !int.TryParse(coordinates.Substring(1), out col))
            {
                return false;
            }

            return true;
        }
    }
}
