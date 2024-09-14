namespace GameDataParser.Helpers;
public class PathBuilder : IPathBuilder
{
    private string _dataFolderName;
    private string _logsFolderName;
    public PathBuilder(string dataFolderName, string logsFolderName)
    {
        _dataFolderName = dataFolderName;
        _logsFolderName = logsFolderName;
    }
    private string GetProjectRootDirectory()
    {
       //Will take to the project root directory and return the full path
        var projectRootDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

        return projectRootDirectory;
    }


    public string BuildFilePath(string fileName)
    {
        return Path.Combine(GetProjectRootDirectory(), _dataFolderName, $"{fileName}.json");
    }

    public string BuildLogFilePath(string logFileName)
    {
        string logsFolderPath = Path.Combine(GetProjectRootDirectory(), _logsFolderName);

        if (!Directory.Exists(logsFolderPath))
        {
            Directory.CreateDirectory(logsFolderPath);
        }
        return Path.Combine(logsFolderPath, logFileName);
    }
}
