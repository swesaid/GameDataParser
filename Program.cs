
public class Program 
{
    public static void Main(string[] args)
    {
        IUserInteraction userInteraction = new ConsoleUserInteraction();
        
        GameDataParserApp App = new GameDataParserApp(userInteraction);
        App.Run();
    }
}

public class GameDataParserApp
{
    private readonly IUserInteraction _userInteraction;

    public GameDataParserApp(IUserInteraction userInteraction)
    {
        _userInteraction = userInteraction;
    }

    public void Run()
    {
        string fileName = _userInteraction.ReadFileNameFromUser();
    }
}

public interface IUserInteraction
{
    string ReadFileNameFromUser();
    void ShowMessage(string message);
    void ShowMessageWithoutNewLine(string message);
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
}

