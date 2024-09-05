
using System.Diagnostics;
using System.Text.Json;

public class Program 
{
    public static void Main(string[] args)
    {

        IUserInteraction userInteraction = new ConsoleUserInteraction();
        IGameDataDeserializer gameDataDeserializer = new GameDataDeserializer(userInteraction);
        IGameDataPrinter gameDataPrinter = new GameDataPrinter(userInteraction);
        IFileReader fileReader = new LocalFileReader();
        ILogger logger = new ApplicationErrorLogger();

        GameDataParserApp App = new GameDataParserApp(userInteraction, gameDataDeserializer, gameDataPrinter, fileReader);

        try
        {
            App.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry! The application has experienced an unexpected error and will have to be closed.");
            logger.Log(ex);
        }

        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }
}

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

public interface IUserInteraction
{
    string ReadFileNameFromUser();
    void ShowMessage(string message);
    void ShowMessageWithoutNewLine(string message);
    void ShowError(string message);
}

public class ConsoleUserInteraction : IUserInteraction
{
    public string ReadFileNameFromUser()
    {
        string fileName = "";
        while (true)
        {
            ShowMessage("Enter the name of the file you want to read: ");
            fileName = Console.ReadLine();

            if (fileName is null)
            {
                ShowMessage("File name cannot be null.\n");
            }
            else if (fileName == "")
            {
                ShowMessage("File name cannot be empty.\n");
            }
            else
            {
                if (!File.Exists(fileName))
                    ShowMessage("File not found.\n");
                else
                    return fileName;
            }
        }
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowMessageWithoutNewLine(string message)
    {
        Console.Write(message);
    }

    public void ShowError(string message)
    {
        //Original colour may not be default colour.
        var originalColour = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColour;
    }
}

public class GameData
{ 
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
    public double Rating { get; init; }


    public override string ToString()
    {
        return $"{Title}, released in {ReleaseYear}, rating: {Rating}";
    }
}

public interface IFileReader
{
    string Read(string fileName);
}

public class LocalFileReader : IFileReader
{
    public string Read(string fileName)
    {
        return File.ReadAllText(fileName);
    }

}

public interface IGameDataDeserializer
{
    List<GameData> Deserialize(string fileName, string jsonContent);
}

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

public interface ILogger
{
    void Log(Exception ex);
}

public class ApplicationErrorLogger : ILogger
{
    private const string _logFileName = "log.txt";
    public void Log(Exception ex)
    {
        using (StreamWriter file = new StreamWriter(_logFileName, append: true))
        {
            file.WriteLine($"[{DateTime.Now}]\nException message: {ex.Message}\nStack trace:{ex.StackTrace}\n");
        }
    }

}

public interface IGameDataPrinter
{
    void Print(List<GameData> games);
}

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