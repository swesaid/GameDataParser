namespace GameDataParser.DataAccess;

public class GameDataDeserializer : IGameDataDeserializer 
{
    private readonly IUserInteraction _userInteraction;
    public GameDataDeserializer(IUserInteraction userInteraction)
    {
        _userInteraction = userInteraction;
    }

    public List<GameData> Deserialize(string fileName, string jsonContent)
    {
        try
        {
            List<GameData> gameData = JsonSerializer.Deserialize<List<GameData>>(jsonContent);
            return gameData;
        }

        catch(JsonException ex) 
        {
            _userInteraction.ShowError($"JSON in the {fileName} was not in a valid format. JSON body: \n{jsonContent}");
            throw new JsonException($"{ex.Message} The file is: {fileName}", ex);
        }
    }
}
