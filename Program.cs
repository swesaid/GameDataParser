
using System.Text.Json;

public class Program 
{
    public static void Main(string[] args)
    {
        try
        {
            IUserInteraction userInteraction = new ConsoleUserInteraction();
            IGameDataRepository gameDataRepository = new GameDataFileRepository(userInteraction);

            GameDataParserApp App = new GameDataParserApp(userInteraction, gameDataRepository);
            App.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry! The application has experienced an unexpected error and will have to be closed.");
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }
    }
}

public class GameDataParserApp
{
    private readonly IUserInteraction _userInteraction;
    private readonly IGameDataRepository _gameDataRepository;

    public GameDataParserApp(IUserInteraction userInteraction, IGameDataRepository gameDataRepository)
    {
        _userInteraction = userInteraction;
        _gameDataRepository = gameDataRepository;
    }

    public void Run()
    {
        //Getting file name from user
        string fileName = _userInteraction.ReadFileNameFromUser();
        
        //Reading the game data from a Json file.
        List<GameData> gameData = _gameDataRepository.Read(fileName);
        
        //Printing the game data.
        _userInteraction.PrintGamesData(gameData);
       
        _userInteraction.ShowMessage("\nPress any key to close.");
         Console.ReadKey();


    }
}

public interface IUserInteraction
{
    string ReadFileNameFromUser();
    void ShowMessage(string message);
    void ShowMessageWithoutNewLine(string message);

    void PrintGamesData(List<GameData> gameDatas);

    void ShowMessageInRed(string message);
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

    public void PrintGamesData(List<GameData> gameDatas)
    {
        if (gameDatas.Count > 0)
        {
            Console.WriteLine("\nLoaded games are: \n");
            foreach (GameData gameData in gameDatas)
                Console.WriteLine(gameData.ToString());
        }
        else
            Console.WriteLine("No games are present in the input file.");

    }
    public void ShowMessageInRed(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
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



public interface IGameDataRepository
{
    List<GameData> Read(string fileName);
}

public class GameDataFileRepository : IGameDataRepository 
{
    private readonly IUserInteraction _userInteraction;
    public GameDataFileRepository(IUserInteraction userInteraction)
    {
        _userInteraction = userInteraction;
    }

    public List<GameData> Read(string fileName)
    {
        string jsonContent = "";
        try
        {
            jsonContent = File.ReadAllText(fileName);
            List<GameData> gameDatas = JsonSerializer.Deserialize<List<GameData>>(jsonContent);
            return gameDatas;
        }

        catch(JsonException ex) 
        {
            _userInteraction.ShowMessageInRed($"JSON in the {fileName} was not in a valid format. JSON body: \n{jsonContent}");
            throw;
        }
    }
}



