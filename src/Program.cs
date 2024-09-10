public class Program 
{
    public static void Main(string[] args)
    {
        string dataFolderName = "data";
        string logsFolderName = "logs";

        IPathBuilder pathBuilder = new PathBuilder(dataFolderName, logsFolderName);
        IUserInteraction userInteraction = new ConsoleUserInteraction(pathBuilder);
        IGameDataDeserializer gameDataDeserializer = new GameDataDeserializer(userInteraction);
        IGameDataPrinter gameDataPrinter = new GameDataPrinter(userInteraction);
        IFileReader fileReader = new LocalFileReader();
        
        ILogger logger = new ApplicationErrorLogger(pathBuilder.BuildLogFilePath("log.txt"));
     
     
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
