public class ApplicationErrorLogger : ILogger
{
    private string _logFileName;

    public ApplicationErrorLogger(string logFileName)
    {
        _logFileName = logFileName;
    }
    public void Log(Exception ex)
    {
        using (StreamWriter file = new StreamWriter(_logFileName, append: true))
        {
            file.WriteLine($"[{DateTime.Now}]\nException message: {ex.Message}\nStack trace:{ex.StackTrace}\n");
        }
    }

}
