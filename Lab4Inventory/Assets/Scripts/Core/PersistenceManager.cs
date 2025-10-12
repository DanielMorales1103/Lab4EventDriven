using System.IO;
using UnityEngine;

public static class PersistenceManager
{
    private static readonly string FilePath =
        Path.Combine(Application.persistentDataPath, "save.json");

    public static bool Save(SaveData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(FilePath, json);
            return true;
        }
        catch (System.Exception e)
        {
            return false;
        }
    }

    public static bool TryLoad(out SaveData data)
    {
        data = null;

        try
        {
            if (!File.Exists(FilePath))
            {
                return false;
            }

            string json = File.ReadAllText(FilePath);
            data = JsonUtility.FromJson<SaveData>(json);
            return data != null;
        }
        catch (System.Exception e)
        {
            return false;
        }
    }

    public static bool SaveExists() => File.Exists(FilePath);

    public static void Delete()
    {
        if (File.Exists(FilePath))
            File.Delete(FilePath);
    }
}
