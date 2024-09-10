namespace GameDataParser.UserInteraction;
public class GameDataPrinter : IGameDataPrinter
{
    private readonly IUserInteraction _userInteraction;

    public GameDataPrinter(IUserInteraction userInteraction)
    {
        _userInteraction = userInteraction;
    }

    public void Print(List<GameData> games)
    {
        if (games.Count > 0)
        {
            _userInteraction.ShowMessage("\nLoaded games are: \n");
            foreach (GameData game in games)
                _userInteraction.ShowMessage(game.ToString());
        }
        else
            _userInteraction.ShowMessage("No games are present in the input file. ");

        _userInteraction.ShowMessage("");

    }
}