namespace GameDataParser.Models;
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
