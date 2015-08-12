namespace Battleships.Common
{
    public class GlobalConstants
    {
        public const char MinRowValueOnGrid = 'A';
        public const char MaxRowValueOnGrid = 'J';
        public const int MinRowValueOnGridAsciiCode = 65;
        public const int GridColsCount = 10;
        public const int GridRowsCount = 10;
        public const int InitialBattleshipsCount = 1;
        public const int InitialDestroyerCount = 2;
        public const int BattleshipSize = 4;
        public const int DestroyerSize = 3;
        public const char BattleshipSymbol = 'X';
        public const char DestroyerSymbol = 'X';
        public const char BlankSymbol = ' ';
        public const char NoShotSymbol = '.';
        public const char MissSymbol = '-';
        public const char HitSymbol = 'X';

        public const string AgreeCommand = "Y";
        public const string ShowCommand = "SHOW";
        public const string ExitCommand = "EXIT";
        public const string NewGameCommand = "NEW";

        public const string EnterCoordinatesMsg = "Enter coordinates (row, col), e.g. A5: ";
        public const string ExitMsg = "Are you sure you want to exit? Press {0} for yes, any other key to continue playing.";
        public const string GameEndMsg = "Well done! You completed the game in {0} shots. \nNew game? Press {1} for yes, any other key to exit: ";

        // Error messages
        public const string InvalidRowMsg = "Row cannot be negative or exceed grid maximum rows.";
        public const string InvalidColMsg = "Column cannot be negative or exceed grid maximum columns.";
        public const string ShipNegativeSizeMsg = "Size cannot be negative.";
        public const string InvalidCommandMsg = "Invalid command. Please enter \"show\", \"exit\", \"new\", or coordinates in the format \"A5\".";
        public const string InvalidShipHit = "The ship is already sunk.";
        public const string InvalidShipMsg = "You cannot create a non-existing ship.";
    }
}
