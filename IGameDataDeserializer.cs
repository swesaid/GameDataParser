public interface IGameDataDeserializer
{
    List<GameData> Deserialize(string fileName, string jsonContent);
}
