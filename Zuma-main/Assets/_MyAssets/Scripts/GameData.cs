using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int SceneIndex;
    public int LifeCount;

    public int UnlockedLevel = 1;

    private static readonly string FilePath = 
        Path.Combine(Application.persistentDataPath, "gamedata.json");

    public static GameData Load()
    {
        try
        {
            return JsonUtility.FromJson<GameData>(File.ReadAllText(FilePath));
        }
        catch
        {
            return null;
        }
    }

    public static void Save(GameData gameData)
    {
        File.WriteAllText(FilePath, JsonUtility.ToJson(gameData));
    }

    public static void Delete()
    {
        File.Delete(FilePath);
    }
}