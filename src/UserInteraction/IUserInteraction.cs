namespace GameDataParser.UserInteraction;

public interface IUserInteraction
{
    string ReadFileNameFromUser();
    void ShowMessage(string message);
    void ShowMessageWithoutNewLine(string message);
    void ShowError(string message);
}
