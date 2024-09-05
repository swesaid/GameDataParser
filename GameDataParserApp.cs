public class GameDataParserApp
{   
    private readonly IUserInteraction _userInteraction;
    private readonly IGameDataDeserializer _gameDataDeserializer;
    private readonly IGameDataPrinter _gameDataPrinter;
    private readonly IFileReader _fileReader;

    public GameDataParserApp(IUserInteraction userInteraction, IGameDataDeserializer gameDataRepository, IGameDataPrinter gameDataPrinter, IFileReader fileReader)
    {
        _userInteraction = userInteraction;
        _gameDataDeserializer = gameDataRepository;
        _gameDataPrinter = gameDataPrinter;
        _fileReader = fileReader;
    }

    public void Run()
    {
        //Getting file name from user
        string fileName = _userInteraction.ReadFileNameFromUser();
        
        //Getting the content from a file.
        string fileContent = _fileReader.Read(fileName);
        
        //Reading the game data from a Json file.
        List<GameData> gameData = _gameDataDeserializer.Deserialize(fileName, fileContent);

        //Printing the game data.
        _gameDataPrinter.Print(gameData);

    }
}
