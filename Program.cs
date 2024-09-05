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
