namespace BattleShips.Console
{
    using Battleships.Logic;
    using Battleships.Logic.Contracts;
    using BattleShips.Logic.Contracts;

    public class BattleshipsMain
    {
        public static void Main()
        {
            IInterface userInterface = new ConsoleInterface();
            IRenderer renderer = new ConsoleRenderer();
            IGameInitializationStrategy gameInitializationStrategy = new GameInitializationStrategy();
            Engine gameEngine = new Engine(renderer, userInterface, gameInitializationStrategy);

            gameEngine.Run();
        }
    }
}
