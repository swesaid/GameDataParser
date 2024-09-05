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
