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
