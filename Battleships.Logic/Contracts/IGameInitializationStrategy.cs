namespace Battleships.Logic.Contracts
{
    using System.Collections.Generic;

    using Battleships.Models;
    using Battleships.Models.Contracts;

    public interface IGameInitializationStrategy
    {
        void Initialize(Grid hiddenGrid, Grid visibleGrid, IList<IShip> ships);
    }
}
