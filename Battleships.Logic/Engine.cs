namespace Battleships.Logic
{
    using System;
    using System.Collections.Generic;

    using Battleships.Common;
    using Battleships.Logic.Contracts;
    using Battleships.Models;
    using Battleships.Models.Contracts;
    using BattleShips.Logic.Contracts;

    public class Engine : IEngine
    {
        private static Random random = new Random();

        private IRenderer renderer;
        private IInterface userInterface;
        private IGameInitializationStrategy gameInitializationStrategy;
        private IList<IShip> ships;
        private Grid visibleGrid;
        private Grid hiddenGrid;
        private GameStatus gameStatus;
        private Position shotPosition;
        private int totalAttempts;

        public Engine(IRenderer renderer, IInterface userInterface, IGameInitializationStrategy gameInitializationStrategy)
        {
            this.renderer = renderer;
            this.userInterface = userInterface;
            this.gameInitializationStrategy = gameInitializationStrategy;
            this.ships = new List<IShip>();
            this.hiddenGrid = new Grid();
            this.visibleGrid = new Grid();
            this.totalAttempts = 0;
            this.gameStatus = GameStatus.Play;
        }

        public IList<IShip> Ships
        {
            get
            {
                return new List<IShip>(this.ships);
            }
        }

        public void Run()
        {
            this.gameInitializationStrategy.Initialize(this.hiddenGrid, this.visibleGrid, this.ships);
            this.renderer.RenderGrid(this.visibleGrid);
            this.renderer.RenderMessage(GlobalConstants.EnterCoordinatesMsg);

            while (true)
            {
                this.renderer.RenderStatusMessage(this.gameStatus.ToString());

                UserCommand command = this.userInterface.GetCommandFromInput();
                this.renderer.ClearError();

                try
                {
                    this.ProcessCommand(command);
                }
                catch (Exception e)
                {
                    this.gameStatus = GameStatus.Error;
                    this.renderer.RenderStatusMessage(this.gameStatus.ToString());
                    this.renderer.RenderErrorMessage(e.Message);
                }

                this.renderer.UpdateGrid(this.visibleGrid, this.shotPosition);

                if (this.AreAllShipsSunk())
                {
                    this.gameStatus = GameStatus.End;
                }

                if (this.gameStatus == GameStatus.End)
                {
                    this.renderer.RenderStatusMessage(this.gameStatus.ToString());
                    this.ProcessGameEnd();
                }
            }
        }

        private void ProcessCommand(UserCommand command)
        {
            switch (command)
            {
                case UserCommand.Show:
                    this.ProcessShowCommand();
                    break;
                case UserCommand.Exit:
                    this.ProcessExitCommand();
                    break;
                case UserCommand.New:
                    this.ProcessNewGame();
                    break;
                case UserCommand.Shoot:
                    this.shotPosition = this.userInterface.GetShotPositionFromInput();
                    this.ProcessShootCommand();
                    break;
                case UserCommand.Invalid:
                default:
                    throw new InvalidOperationException(GlobalConstants.InvalidCommandMsg);
            }
        }

        private void ProcessShowCommand()
        {
            this.gameStatus = GameStatus.Show;
            this.renderer.RenderStatusMessage(this.gameStatus.ToString());
            this.renderer.RenderGrid(this.hiddenGrid);
            this.ProcessCommand(this.userInterface.GetCommandFromInput());
            this.renderer.RenderGrid(this.visibleGrid);
        }

        private void ProcessShootCommand()
        {
            if (this.hiddenGrid.GetCell(this.shotPosition) != GlobalConstants.BlankSymbol)
            {
                this.ProcessShipHit();
                this.visibleGrid.SetCell(this.shotPosition, GlobalConstants.HitSymbol);
            }
            else
            {
                this.visibleGrid.SetCell(this.shotPosition, GlobalConstants.MissSymbol);
                this.gameStatus = GameStatus.Miss;
            }

            this.totalAttempts++;
        }

        private void ProcessShipHit()
        {
            for (int i = 0; i < this.Ships.Count; i++)
            {
                var currentShip = this.Ships[i];
                if (this.IsShipHit(currentShip, this.shotPosition) && this.visibleGrid.GetCell(this.shotPosition) != GlobalConstants.HitSymbol)
                {
                    this.HitShip(currentShip);
                    this.gameStatus = GameStatus.Hit;

                    if (this.IsShipSinking(currentShip))
                    {
                        this.SinkShip(currentShip);
                        this.gameStatus = GameStatus.Sunk;
                    }
                }
            }
        }

        private bool IsShipHit(IShip ship, Position position)
        {
            var row = ship.TopLeft.Row;
            var col = ship.TopLeft.Col;

            for (int j = 0; j < ship.Size; j++)
            {
                if (position.Row == row && position.Col == col)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Horizontal)
                {
                    col++;
                }
                else
                {
                    row++;
                }
            }

            return false;
        }

        private void HitShip(IShip ship)
        {
            ship.HitsCount++;

            if (ship.HitsCount > ship.Size)
            {
                throw new InvalidOperationException(GlobalConstants.InvalidShipHit);
            }
        }

        private bool IsShipSinking(IShip ship)
        {
            if (ship.HitsCount == ship.Size)
            {
                return true;
            }

            return false;
        }

        private void SinkShip(IShip ship)
        {
            ship.IsSunk = true;
        }

        private void ProcessExitCommand()
        {
            this.renderer.RenderMessage(string.Format(GlobalConstants.ExitMsg, GlobalConstants.AgreeCommand));
            UserCommand command = this.userInterface.GetCommandFromInput();

            if (command == UserCommand.Agree)
            {
                this.userInterface.ExitGame();
            }
            else
            {
                this.renderer.RenderMessage(GlobalConstants.EnterCoordinatesMsg);
            }
        }

        private bool AreAllShipsSunk()
        {
            for (int i = 0; i < this.Ships.Count; i++)
            {
                if (!this.Ships[i].IsSunk)
                {
                    return false;
                }
            }

            return true;
        }

        private void ProcessGameEnd()
        {
            this.renderer.RenderMessage(string.Format(GlobalConstants.GameEndMsg, this.totalAttempts, GlobalConstants.AgreeCommand), false);
            UserCommand command = this.userInterface.GetCommandFromInput();
            if (command == UserCommand.Agree)
            {
                this.ProcessNewGame();
            }
            else
            {
                this.userInterface.ExitGame();
            }
        }

        private void ProcessNewGame()
        {
            this.gameStatus = GameStatus.Play;
            this.renderer.Clear();
            this.totalAttempts = 0;
            this.Run();
        }
    }
}
